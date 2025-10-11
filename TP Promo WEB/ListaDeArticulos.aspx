<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListaDeArticulos.aspx.cs" Inherits="TP_Promo_WEB.ListaDeArticulos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	    <div class="row row-cols-1 row-cols-3 g-4">
        <asp:Repeater ID="repetidorDeArticulos" runat="server">
            <ItemTemplate>
                <div class="col">
                    <div class="card border-dark mb-3" style="max-width: 20rem;">
                 
                        <img src="<%# ((List<Dominio.Imagen>)Eval("Imagenes"))[0].ImagenUrl %>>" class="card-img-top" alt="..." style="height: 400px; object-fit: contain;" >
              
                        <div class="card-body">                       
                            <h5 class="card-title"><%# Eval("NombreArticulo") %></h5>
                            <p class="card-text"><%# Eval("DescripcionArticulo") %></p>
                            <h4 class="card-title" style="color: blue; text-align: right;">
                                $<%# Eval("PrecioArticulo", "{0:F2}") %>
                            </h4>      
                            
							<a href='DetalleDeArticulos.aspx?id=<%# Eval("IdArticulo") %>' class="btn btn-primary">Detalle</a>

                            <asp:Button runat="server" ID="btnContinuar" CssClass="btn btn-primary" Text="Continuar" CommandName="VerDetalle" OnClick="btnContinuar_Click" CommandArgument='<%# Eval("IdArticulo") %>' />

                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
