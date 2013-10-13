$().ready(function () {

    UmbracoSpeechBubble.prototype.DefaultShowMessage = UmbracoSpeechBubble.prototype.ShowMessage;

    UmbracoSpeechBubble.prototype.ShowMessage = function (icon, header, message, dontAutoHide) {
        header = header.replace(/error/i, "Info");
        if ($('#' + this.id).is(':visible')) {
            jQuery("#" + this.id + "Message").append('<h3>' + header + '</h3><p>' + message + '</p>');
        } else {
            this.DefaultShowMessage(icon, header, message, dontAutoHide);
        }
    };

    UmbSpeechBubble = new UmbracoSpeechBubble("defaultSpeechbubble");
});