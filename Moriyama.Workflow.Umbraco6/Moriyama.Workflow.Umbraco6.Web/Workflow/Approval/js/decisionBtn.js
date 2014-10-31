$("#rejectWorkflow").click(function () {

  wf.Approve(wf.activeItem, false);
  $('.rejectUi').show();

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
  $('.rejectUi').hide();

  $('ul.side-nav li.active i').removeClass('fi-x');
  $('ul.side-nav li.active i').addClass('fi-check');
  $("#rejectionReason").addClass("hide");
});

$('.accept-btn a').click(function() {

    $(wf.fieldId).val(JSON.stringify(wf.nodeDetails));
    $(wf.buttonId).trigger('click');
});

wf.setArea = function (id) {
    $(wf.nodeDetails).each(function () {
        if (id == this.Id) {
            $('.rejectArea').val(this.Comment);
            if (this.Approved) {
                $('.rejectUi').hide();
                $('input[name=decision][value=Accept]').prop("checked", true);
                
            } else {
                $('.rejectUi').show();
                $('input[name=decision][value=Reject]').prop("checked", true);
            }
        }
    });
};

wf.setComment = function(id, comment) {
    $(wf.nodeDetails).each(function () {
        if (id == this.Id) {
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
};