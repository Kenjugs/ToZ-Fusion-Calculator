<%@ Page Title="" Language="C#" MasterPageFile="Template.master" AutoEventWireup="true" CodeBehind="Simulator.aspx.cs" Inherits="Skill_Calculator.Simulator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <asp:SqlDataSource ID="dsSkillNames" runat="server"></asp:SqlDataSource>
    <main class="mainBody">
        <div class="table-div outer">
            <div class="tablerow-div">
                <div class="tablecell-div">
                    <div class="table-div inner-bordered">
                        <!-- first form -->
                        <div class="tablerow-div top-row">
                            <div class="tablecell-div">
                                <div style="height: 50px; width: 50px; display: inline-block; background-color: green; vertical-align: middle;"></div>
                            </div>
                            <div class="tablecell-div">
                                Summary text of instructions maybe. Possibly item descriptions later.
                            </div>
                        </div>
                    </div>
                    <div class="table-div inner">
                        <div class="tablerow-div">
                            <div class="tablecell-div">
                                <asp:DropDownList ID="ddlLeft1" runat="server" Width="100%"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="tablerow-div">
                            <div class="tablecell-div">
                                <asp:DropDownList ID="ddlLeft2" runat="server" Width="100%"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="tablerow-div">
                            <div class="tablecell-div">
                                <asp:DropDownList ID="ddlLeft3" runat="server" Width="100%"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="tablerow-div">
                            <div class="tablecell-div">
                                <asp:DropDownList ID="ddlLeft4" runat="server" Width="100%"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="tablecell-div">
                    <div class="table-div inner-bordered">
                        <!-- second form -->
                        <div class="tablerow-div top-row">
                            <div class="tablecell-div">
                                <div style="height: 50px; width: 50px; display: inline-block; background-color: green; vertical-align: middle;"></div>
                            </div>
                            <div class="tablecell-div">
                                Summary text of instructions maybe. Possibly item descriptions later.
                            </div>
                        </div>
                    </div>
                    <div class="table-div inner">
                        <div class="tablerow-div">
                            <div class="tablecell-div">
                                <asp:DropDownList ID="DropDownList1" runat="server" Width="100%"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="tablerow-div">
                            <div class="tablecell-div">
                                <asp:DropDownList ID="DropDownList2" runat="server" Width="100%"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="tablerow-div">
                            <div class="tablecell-div">
                                <asp:DropDownList ID="DropDownList3" runat="server" Width="100%"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="tablerow-div">
                            <div class="tablecell-div">
                                <asp:DropDownList ID="DropDownList4" runat="server" Width="100%"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
