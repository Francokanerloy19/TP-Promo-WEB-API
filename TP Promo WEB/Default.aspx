<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TP_Promo_WEB._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .contenedor {
            display: flex;
            justify-content: center; /* horizontal */
            align-items: center; /* vertical */
            min-height: calc(100vh - 160px); /* resta header + footer */
        }

        .txt-largo {
            width: 400px; /* o en % para que se adapte */
            max-width: 100%;
        }
    </style>
    <script>   
    function validar() {
        const textCodigo = document.getElementById("<%= TextCodigo.ClientID %>");
        const exRegularCodigo = /^[A-Za-z0-9]{6,12}$/;
        

        let valido = true;

        if (!exRegularDNI.test(textCodigo.value.trim())) {
            textCodigo.classList.add("is-invalid");
            textCodigo.classList.remove("is-valid");
            valido = false;
        } else {
            textCodigo.classList.remove("is-invalid");
            textCodigo.classList.add("is-valid");

        }
        

        return valido;


    }


    </script>
    <hr />
    <div class="contenedor">
        <div">
            <label for="TextCodigo" class="form-label">Ingresá el código de tu voucher!</label>
            <asp:TextBox runat="server" ID="TextCodigo" CssClass="form-control txt-largo" placeholder="xxxxxxxx" />
            <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="TextCodigo" ErrorMessage="El código es obligatorio" CssClass="text-danger" Display="Dynamic" />
            <asp:RegularExpressionValidator ID="revNombre" runat="server" ControlToValidate="TextCodigo" ValidationExpression="^[A-Za-z0-9]{6,12}$" ErrorMessage="El voucher es invalido" Display="Dynamic" CssClass="text-danger"/>
            <br />
            <asp:Button Text="Siguiente" ID="btnSiguiente" runat="server" class="btn btn-primary" OnClientClick="return validar()" OnClick="btnSiguiente_Click" />
            <br />
            <asp:Label ID="lblMensajeError" runat="server" CssClass="text-danger" />
        </div>
    </div>
</asp:Content>
