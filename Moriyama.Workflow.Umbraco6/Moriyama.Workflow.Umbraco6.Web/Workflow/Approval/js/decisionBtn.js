$("#rejectWorkflow").click(function() {
  $('ul.side-nav li.active i').removeClass('fi-check');
  $('ul.side-nav li.active i').addClass('fi-x');
  $("#rejectionReason").removeClass("hide");
});

$("#acceptWorkflow").click(function() {
  $('ul.side-nav li.active i').removeClass('fi-x');
  $('ul.side-nav li.active i').addClass('fi-check');
  $("#rejectionReason").addClass("hide");
});