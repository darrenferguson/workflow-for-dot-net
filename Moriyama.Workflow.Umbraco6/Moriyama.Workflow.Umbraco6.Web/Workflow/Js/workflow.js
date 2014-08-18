var wf = {

    'id': 1,
    'endPointId': 1,
    'endPointCol': '#aaaaaa',

    'tasks': {},
    'transitions': [],
    'endPoints': {},
    'targetEndPoints': {},

    'dropOptions': {
        tolerance: 'touch',
        hoverClass: 'dropHover',
        activeClass: 'dragActive'
    }
};

wf.closeTemplate = '<div class="close">x</div>';

$().ready(function () {

    $('.tasks').height($(window).height() - 40);

    jsPlumb.Defaults.Connector = ["Bezier", { curviness: 50}];
    jsPlumb.Defaults.DragOptions = { cursor: 'pointer' };
    jsPlumb.Defaults.PaintStyle = { strokeStyle: wf.endPointCol, lineWidth: 1 };
    jsPlumb.Defaults.EndpointStyle = { width: 10, height: 10, strokeStyle: wf.endPointCol };
    jsPlumb.Defaults.Endpoint = ["Rectangle", { radius: 20}];
    jsPlumb.Defaults.Anchors = ["TopCenter", "TopCenter"];

    wf.getConnectorOverlay = function (label) {
        return [
            "Arrow",
            ["Label", { label: label, location: 0.75}]
        ];
    };

    wf.getSrcEndPoint = function (label, transitionId, taskId) {

        var endPointId = 'ep' + (wf.endPointId++);
        if (!wf.endPoints[taskId]) { wf.endPoints[taskId] = {}; }

        var ep = {
            uuid: endPointId,
            isSource: true,
            scope: 'workflow',
            maxConnections: 1,
            isTarget: false,
            dropOptions: wf.dropOptions,
            connectorOverlays: wf.getConnectorOverlay(label)
        };
        wf.endPoints[taskId][endPointId] = transitionId;
        return ep;
    };

    wf.addTargetEndPoints = function (taskId) {

        var endPointId = 'ep' + (wf.endPointId++);

        var trgEndPoint = {
            uuid: endPointId,
            isSource: false,
            scope: 'workflow',
            anchor: 'TopCenter',
            maxConnections: 1,
            isTarget: true,
            dropOptions: wf.dropOptions
        };

        var p = jsPlumb.addEndpoint(taskId, { anchor: "TopCenter" }, trgEndPoint);
        wf.targetEndPoints[taskId] = p;

        $('._jsPlumb_endpoint').css('z-index', '9999');
    };

    wf.addEndPoints = function (taskId, availableTransitions, transitionInfo, transitions) {

        var transitionIndex = 0;

        for (var transition in transitionInfo) {

            var position = (availableTransitions == 1) ? 'BottomCenter' : [((0.5 / availableTransitions) + ((1 / availableTransitions) * transitionIndex)), 1, 0, 1];

            var p = jsPlumb.addEndpoint(
                    taskId,
                    jsPlumb.extend({
                        anchor: position
                    },
                    wf.getSrcEndPoint(
                            transitionInfo[transition],
                            transition,
                            taskId)
                    )
                );

            if (transitions) {
                if (transitions[transition]) {
                    var q = wf.targetEndPoints[transitions[transition]];
                    jsPlumb.connect({ sourceEndpoint: p, targetEndpoint: q,
                        overlays: wf.getConnectorOverlay(transitionInfo[transition])
                    });
                }
            }
            transitionIndex++;
        }

        $('._jsPlumb_endpoint').css('z-index', '9999');
        jsPlumb.draggable(taskId);
    };



    $(wf.saveButton).click(function () {

        $.each(wf.config.UiTasks, function (taskId, task) {
            task['Top'] = parseInt($('#' + taskId).offset().top);
            task['Left'] = parseInt($('#' + taskId).offset().left);
        });

        $(wf.tasksField).val(JSON.stringify(wf.config.UiTasks));
        var connections = jsPlumb.getConnections('workflow');

        $(connections).each(function () {
            var epId = this.endpoints[0].getUuid();
            var transition = wf.endPoints[this.sourceId][epId];
            wf.transitions.push({ 'source': this.sourceId, 'target': this.targetId, 'transition': transition });
        });

        $(wf.transitionsField).val(JSON.stringify(wf.transitions));
    });

    $(".designer").droppable({
        drop: function (event, ui) {

            if ($(ui.draggable).hasClass('design')) return;

            $('.dropInfo').hide();

            var taskId = wf.guidPool.shift();
            var taskType = $(ui.draggable).attr('id');

            var x = ui.offset.left;
            var y = ui.offset.top;

            $(this).append(
                $(ui.draggable).clone()
                    .css('position', 'absolute')
                    .css('left', x)
                    .css('top', y)
                    .css('z-index', 1000)
                    .attr('id', taskId)
                    .addClass('design')
                    .prepend(wf.closeTemplate)
                    .removeClass('ui-draggable')
                    .removeClass('ui-draggable-dragging')
            );

            var taskTemplate = jQuery.extend(true, {}, wf.taskInfo.Tasks[taskType]);

            taskTemplate['Id'] = taskId;
            taskTemplate['AvailableTransitions'] = [];
            taskTemplate['Transitions'] = {};
            taskTemplate['CustomProperties'] = {};

            taskTemplate['Description'] = '';
            taskTemplate['Top'] = x;
            taskTemplate['Left'] = y;

            wf.config.UiTasks[taskId] = taskTemplate;

            wf.config.UiTasks[taskId] = taskTemplate;

            var availableTransitions = parseInt(wf.taskInfo.Tasks[taskType].AvailableTransitions);
            var transitionInfo = wf.taskInfo.Tasks[taskType].TransitionDescriptions;

            wf.addTargetEndPoints(taskId);
            wf.addEndPoints(taskId, availableTransitions, transitionInfo);
        }
    });

    $('div.close').live('click', function () {

        var taskId = $(this).parent().attr('id');
        jsPlumb.detachAll(taskId);
        jsPlumb.removeAllEndpoints(taskId);

        if ($(this).parent().hasClass('selected')) {
            $('#propertiesFrame').attr('src', 'about:blank');
        }

        $(this).parent().remove();
        delete (wf.config.UiTasks[taskId]);

        return false;
    });

    $('.design').live('click', function () {

        $('.design').removeClass('selected');
        $(this).addClass('selected');

        var taskId = $(this).attr('id');
        var config = wf.config.UiTasks[taskId];

        var params = config['CustomProperties'] ? jQuery.extend(true, {}, config['CustomProperties']) : {};
        params['Name'] = config['Name'];
        params['Description'] = config['Description'];
        params['Id'] = config['Id'];
        params['IsStartTask'] = config['IsStartTask'];
        params['AssemblyQualifiedTypeName'] = config['AssemblyQualifiedTypeName'];

        var qs = $.param(params);

        $('#propertiesFrame').attr('src', 'EditTaskProperties.aspx?' + qs);
    });

    wf.setTaskProperties = function (taskId, props) {

        for (var prop in props) {
            if (prop in wf.config.UiTasks[taskId]) {
                wf.config.UiTasks[taskId][prop] = props[prop];
            } else {
                wf.config.UiTasks[taskId]['CustomProperties'][prop] = props[prop];
            }
        }
        // alert(JSON.stringify(wf.config.UiTasks[taskId]));
        $('#' + taskId + ' > div[class=displayEntityName]').html(props['Name']);
    };

    document.onselectstart = function () { return false; };

    $('.draggable').draggable({ revert: true,

        appendTo: 'body',
        containment: 'window',
        scroll: false,
        helper: 'clone'

    });

    $.each(wf.config.UiTasks, function (taskId) {
        wf.addTargetEndPoints(taskId);
    });

    $.each(wf.config.UiTasks, function (taskId, task) {
        wf.addEndPoints(taskId, task.AvailableTransitions.length, task.TransitionDescriptions, task.Transitions);
    });
});