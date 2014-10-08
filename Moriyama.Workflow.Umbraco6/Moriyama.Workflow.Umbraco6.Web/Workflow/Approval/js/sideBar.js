$( "#pages-for-approval > div" ).each(function() {;
    $("ul.side-nav").append("<li data-id='" + $(this).attr('data-id') + "'>" + "<a href='#'>" + $(this).attr('data-name') + "<i class='fi-check'></i></a>" + "</li><span class='icon'></span>");
});