<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TabRefresh.ascx.cs" Inherits="FergusonMoriyam.Workflow.Umbraco.Web.Ui.TabRefresh" %>

<script type="text/javascript">
    <!--
    $().ready(function () {

        var epoch = Number('<%= TheHelper.Epoch() %>');
       
        var tab = '<%= Request["tab"] %>';
        var reqEpoch = Number('<%= Request["epoch"] %>');

        var diff = epoch - reqEpoch;
       
        if ((tab.length > 0) && (diff < 3000)) {
            $('#' + tab).click();
        }
    });
    -->
</script>