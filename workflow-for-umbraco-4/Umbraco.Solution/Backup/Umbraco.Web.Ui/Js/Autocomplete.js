
$().ready(function () {

    $('.eventBrowser').live('keyup', function (e) {

        var id = $(this).attr('rel');
        var selected = new Array();

        $('option:selected', id).each(function () {
            selected.push($(this).val());
        });

        $(id).empty();
        var v = $(this).val().toLowerCase();
        $(selected).each(function () {
            $(id).append($("<option/>", {
                value: this,
                text: this,
                selected: true
            }));
        });

        $(fmEvt).each(function () {

            if (this.FullName.toLowerCase().indexOf(v) > -1) {
                $(id).append($("<option/>", {
                    value: this.FullName,
                    text: this.FullName
                }));
            }
        });

        if ($('option:selected', id).size() == 0) { }
        return false;
    });

    $('.evtList').each(function () {
        $(this).before('<p><input type="text" placeholder="Type to filter...." class="eventBrowser" rel="#' + $(this).attr('id') + '"/></p>');
        $(this).css('height', '180px');
        $(this).css('width', '90%');
    });
})