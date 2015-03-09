$("#pages-for-approval > div").each(function () {

    var ref = $(this).attr('data-refer');
    var mod = '';

    if (ref.length > 0) {
        mod += ' ';
        var c = 1;

        $(ref.split(',')).each(function() {
            mod += "[" + c + "] ";
            c++;
        });
    }

    $("ul.side-nav").append("<li data-id='" + $(this).attr('data-id') + "'><a href='#'>" + $(this).attr('data-name') + "<i class='fi-check'></i></a></li><span class='icon'></span>");
});