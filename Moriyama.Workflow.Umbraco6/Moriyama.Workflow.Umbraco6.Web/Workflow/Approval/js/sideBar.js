$( "#pages-for-approval > div" ).each(function() {
	var plusOne = $(this).index();
	var plus = plusOne+1;
	$("ul.side-nav").append("<li>" + "<a href='#'>" + "Page " + plus + "<i></i></a>" + "</li><span class='icon'></span>");
});