$(document).ready(function(){   

    //height of dom
    var iframeHeight = $(document).height();
    $(".approval-section").css("height", iframeHeight+"px");

    // on click of side nav item 
    $("ul.side-nav li").click(function() {
        
        wf.activeItem = $(this).attr('data-id');
        wf.setArea(wf.activeItem);

        //count side nav this will be used with :nth-child to go through the  correct tabs
        var tabNum = $(this).index("ul.side-nav li");

        //zero based so add one so the list doesn't start from nought
        var nthChild = tabNum+1;

        //show the radio btns
        $(".radio").removeClass("hide");

        $("ul.side-nav li.active").removeClass("active");
        $(this).addClass("active");

        $("#pages-for-approval div.page").removeClass("active");
        $("#pages-for-approval div.page:nth-child(" + nthChild + ")").addClass("active");

        var a = $("#pages-for-approval div.page:nth-child(" + nthChild + ") iframe");
        $(a).attr('src', $(a).attr('src'));

        var iframeWidth = $(".approval-section").width();
        
        $("#pages-for-approval div.page iframe").css({ "width": iframeWidth - 10 + "px", "height": iframeHeight - 100 + "px" });
        
    });

    $("ul.side-nav li").first().trigger('click');
});
