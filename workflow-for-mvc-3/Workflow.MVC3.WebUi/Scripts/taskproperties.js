$().ready(function () {
    if (typeof (wf.taskProps) != 'undefined') {
        window.parent.wf.setTaskProperties(wf.TaskId, wf.taskProps);
    }
});