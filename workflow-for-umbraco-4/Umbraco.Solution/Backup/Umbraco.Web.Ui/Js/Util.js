var wutil = {};

wutil.pluginPath = 'plugins/fmworkflow/';
wutil.modalWidth = 640;
wutil.modalHeight = 500;

String.prototype.format = function () {
    var formatted = this;
    for (var i = 0; i < arguments.length; i++) {
        var regexp = new RegExp('\\{' + i + '\\}', 'gi');
        formatted = formatted.replace(regexp, arguments[i]);
    }
    return formatted;
};

wutil.addTabToUrl = function (url, tab) {

    url = url.replace(/[\&\?]tab\=.*?$/, '');

    if (url.indexOf('?') > -1) {
        url = url + '&tab=' + tab;
    } else {
        url = url + '?tab=' + tab;
    }

    url += '&epoch=' + escape(new Date().getTime());
    return url;
};

wutil.getTab = function(id) {
    return $('#' + id).closest('.tabpage').attr('id').replace( /(.*)layer/ , '$1') + 'a';
};

wutil.debug = function (s) {
    console.log(s);
};