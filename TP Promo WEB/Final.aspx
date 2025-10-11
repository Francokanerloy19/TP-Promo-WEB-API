<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Final.aspx.cs" Inherits="TP_Promo_WEB.Final" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .contenedor-final {
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
            min-height: calc(100vh - 160px); /* ajusta el espacio según header/footer */
            text-align: center;
        }
    </style>

    <div class="contenedor-final">
        <hr />
        <div>
            <h3>Muchas gracias por participar!!!</h3>
        </div>
        <div>
            <label for="txtVolver" class="form-label">Posdes regresar al incio presionando volver.</label>
            <br />

            <asp:Button Text="Volver" ID="btnVolver" runat="server" class="btn btn-primary" OnClick="btnVolver_Click" />

        </div>
    </div>

</asp:Content>
