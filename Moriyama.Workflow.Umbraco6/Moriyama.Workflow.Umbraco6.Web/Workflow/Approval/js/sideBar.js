    var indo = 1;
    $("#pages-for-approval > div").each(function() {

        var ref = $(this).attr('data-refer');
        var mod = '';


        if (ref.length > 0) {
            mod += '<p>';
            var c = 1;

            $(ref.split(',')).each(function() {
                mod += "<a href='/umbraco/dialogs/preview.aspx?id=" + this + "' class='moduleReference' data-position='"+indo+"'><small>Reference " + c + "</small></a>";
                c++;
            });
            mod += '</p>';
        }
        indo++;

        $("ul.side-nav").append("<li data-id='" + $(this).attr('data-id') + "'><a href='#'>" + $(this).attr('data-name') + "<i class='fi-check'></i></a>" + mod + "</li><span class='icon'></span>");
    });
