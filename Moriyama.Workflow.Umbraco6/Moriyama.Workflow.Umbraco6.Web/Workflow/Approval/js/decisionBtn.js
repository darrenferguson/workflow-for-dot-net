$("#rejectWorkflow").click(function () {

  wf.Approve(wf.activeItem, false);
  // wf.setArea(wf.activeItem);
  
  $('ul.side-nav li.active i').removeClass('fi-check');
  $('ul.side-nav li.active i').addClass('fi-x');

  $("#rejectionReason").removeClass("hide");

});

$('.rejectArea').change(function() {
    wf.setComment(wf.activeItem, $(this).val());
    console.log(wf.nodeDetails);
});

$("#acceptWorkflow").click(function () {

  wf.Approve(wf.activeItem, true);

  $('ul.side-nav li.active i').removeClass('fi-x');
  $('ul.side-nav li.active i').addClass('fi-check');
  $("#rejectionReason").addClass("hide");
});

wf.setArea = function (id) {
    $(wf.nodeDetails).each(function () {
        if (id == this.Id) {
            alert(this.Comment + id);
            $('.rejectArea').val(this.Comment);
        }
    });
};

wf.setComment = function(id, comment) {
    $(wf.nodeDetails).each(function () {
        if (id == this.Id) {
            alert(comment + " " + id);
            this.Comment = comment;
        }
    });
};


wf.Approve = function (id, status) {

    $(wf.nodeDetails).each(function() {
        if (id == this.Id) {
            this.Approved = status;
        }
    });
    console.log(wf.nodeDetails);
};