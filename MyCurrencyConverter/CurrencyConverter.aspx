<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CurrencyConverter.aspx.cs" Inherits="MyCurrencyConverter.CurrencyConverter" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            From: &nbsp;
            <input type="text" id="GBP" runat="server" />
            &nbsp; GBP (U.K. pounds) to: &nbsp;

            <select id="Currency" runat="server" />
            <br /><br />

            <input type="submit" value="Convert" id="Converter"
                OnServerClick="Convert_Server" runat="server" />
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;

            <asp:Button id="resetAll" runat="server" OnClick="ResetControls" Text="Reset..." ></asp:Button>              
            <br /><br />

            <div style="font-weight: bold" id="Result" runat="server"></div>

            <div>
                <br /><br />
                    From Date: &nbsp; 
                <asp:TextBox id="txtdpd1" runat="server" ></asp:TextBox>
                <asp:Calendar id="datePicker1" runat="server" OnSelectionChanged="datePicker1_SelectionChanged"></asp:Calendar>
                <br /><br />
                    To Date: &nbsp;
                <asp:TextBox id="txtdpd2" runat="server" ></asp:TextBox>
                <asp:Calendar id="datePicker2" runat="server" OnSelectionChanged="datePicker2_SelectionChanged"></asp:Calendar>
            </div>

            <div>
                <br /><br />
                <asp:Button id="Search" runat="server" OnClick="Search_DateRange" Text="Search..." ></asp:Button>
                <br /><br />
            </div>

            Logging Data: &nbsp;
            <asp:GridView id="gridView" runat="server" ></asp:GridView>
 
        </div>
    </form>
</body>
</html>
