var wf = {};

$().ready(function () {

    wf.editProperties = function (id, t, tab) {
        var url = wutil.pluginPath + 'EditConfigurationProperties.aspx?id={0}&tab={1}'.format(escape(id), escape(tab));
        return wf.openWindow(url, t);
    };

    wf.editCriteria = function (id, t, tab) {
        var url = wutil.pluginPath + 'EditInstantiationCriteria.aspx?id={0}&tab={1}'.format(escape(id), escape(tab));
        return wf.openWindow(url, t);
    };

    wf.sendToWorkflow = function (id, t, tab) {
        var url = wutil.pluginPath + 'SendToWorkflow.aspx?ids={0}&tab={1}'.format(escape(id), escape(tab));
        return wf.openWindow(url, t);
    };

    wf.openComments = function (id, t, tab) {
        var url = wutil.pluginPath + 'Comments.aspx?id={0}&tab={1}'.format(escape(id), escape(tab));
        return wf.openWindow(url, t);
    };

    wf.transition = function (url, t, tab) {
        url += '&tab={0}'.format(escape(tab));
        
        return wf.openWindow(url, t);
    };

    wf.openWindow = function (url, t) {
        UmbClientMgr.openModalWindow(url, t, true, wutil.modalWidth, $(window).height() - 100, 50);
        return false;
    };

});
