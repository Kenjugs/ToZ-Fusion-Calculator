<%@ Page Title="" Language="C#" MasterPageFile="Template.master" AutoEventWireup="true" CodeBehind="Simulator.aspx.cs" Inherits="Skill_Calculator.Simulator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function() {
            var watch = $('select[id*="ddlLeft"], select[id*="ddlRight"]');
            var sender = "";
            var other = "";
            var label = "";

            watch.each(function(idx, el) {
                $(this).on("change", function(ev) {
                    if (/ddlleft\d/i.test($(this).prop("id"))) {
                        sender = $.param($(this));
                        other = $.param($("#" + $(this).prop("id").replace(/ddlLeft(\d)/, "ddlRight$1")));
                        label = $("#" + $(this).prop("id").replace(/ddlLeft(\d)/, "lblResult$1"));
                    } else if (/ddlright\d/i.test($(this).prop("id"))) {
                        sender = $.param($(this));
                        other = $.param($("#" + $(this).prop("id").replace(/ddlRight(\d)/, "ddlLeft$1")));
                        label = $("#" + $(this).prop("id").replace(/ddlRight(\d)/, "lblResult$1"));
                    }

                    $.ajax({
                        method: "POST",
                        url: "Simulator.aspx/Calculate",
                        data: "{ sender: " + JSON.stringify(sender) + ", other: " + JSON.stringify(other) + " }",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function(data) { label.text(data.d); },
                        async: true,
                        cache: false
                    });
                });
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <asp:SqlDataSource ID="dsSkillNames" runat="server"></asp:SqlDataSource>
    <main class="mainBody">
        <table class="table outer">
            <tr>
                <td class="tablecell">
                    <table class="table inner-bordered">
                        <tr class="tablerow top-row">
                            <td class="tablecell">
                                <div style="height: 50px; width: 50px; display: inline-block; background-color: green; vertical-align: middle;"></div>
                            </td>
                            <td class="tablecell">Summary text or instructions maybe. Possibly item descriptions later.
                            </td>
                        </tr>
                    </table>
                    <table class="table inner">
                        <tr class="tablerow">
                            <td class="tablecell">
                                <asp:DropDownList ID="ddlLeft1" runat="server" Width="100%" AppendDataBoundItems="true">
                                    <asp:ListItem Text="(BLANK)" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="tablerow">
                            <td class="tablecell">
                                <asp:DropDownList ID="ddlLeft2" runat="server" Width="100%" AppendDataBoundItems="true">
                                    <asp:ListItem Text="(BLANK)" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="tablerow">
                            <td class="tablecell">
                                <asp:DropDownList ID="ddlLeft3" runat="server" Width="100%" AppendDataBoundItems="true">
                                    <asp:ListItem Text="(BLANK)" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="tablerow">
                            <td class="tablecell">
                                <asp:DropDownList ID="ddlLeft4" runat="server" Width="100%" AppendDataBoundItems="true">
                                    <asp:ListItem Text="(BLANK)" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="tablecell">
                    <table class="table inner-bordered">
                        <!-- right side -->
                        <tr class="tablerow top-row">
                            <td class="tablecell">
                                <div style="height: 50px; width: 50px; display: inline-block; background-color: green; vertical-align: middle;"></div>
                            </td>
                            <td class="tablecell-div">Summary text or instructions maybe. Possibly item descriptions later.
                            </td>
                        </tr>
                    </table>
                    <table class="table inner">
                        <tr class="tablerow">
                            <td class="tablecell">
                                <asp:DropDownList ID="ddlRight1" runat="server" Width="100%" AppendDataBoundItems="true">
                                    <asp:ListItem Text="(BLANK)" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="tablerow">
                            <td class="tablecell">
                                <asp:DropDownList ID="ddlRight2" runat="server" Width="100%" AppendDataBoundItems="true">
                                    <asp:ListItem Text="(BLANK)" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="tablerow">
                            <td class="tablecell">
                                <asp:DropDownList ID="ddlRight3" runat="server" Width="100%" AppendDataBoundItems="true">
                                    <asp:ListItem Text="(BLANK)" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="tablerow">
                            <td class="tablecell">
                                <asp:DropDownList ID="ddlRight4" runat="server" Width="100%" AppendDataBoundItems="true">
                                    <asp:ListItem Text="(BLANK)" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="tablecell">
                    <table class="table inner-bordered results">
                        <!-- results -->
                        <tr class="tablerow top-row">
                            <td class="tablecell">
                                <div style="height: 50px; width: 50px; display: inline-block; background-color: green; vertical-align: middle;"></div>
                            </td>
                            <td class="tablecell-div">Results of fusion go here
                            </td>
                        </tr>
                    </table>
                    <table class="table inner results">
                        <tr class="tablerow">
                            <td class="tablecell">1)
                            </td>
                            <td class="tablecell centered">
                                <asp:Label ID="lblResult1" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr class="tablerow">
                            <td class="tablecell">2)
                            </td>
                            <td class="tablecell centered">
                                <asp:Label ID="lblResult2" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr class="tablerow">
                            <td class="tablecell">3)
                            </td>
                            <td class="tablecell centered">
                                <asp:Label ID="lblResult3" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr class="tablerow">
                            <td class="tablecell">4)
                            </td>
                            <td class="tablecell centered">
                                <asp:Label ID="lblResult4" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </main>
</asp:Content>
