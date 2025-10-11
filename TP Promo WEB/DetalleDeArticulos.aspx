<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetalleDeArticulos.aspx.cs" Inherits="TP_Promo_WEB.DetalleDeArticulos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card mb-3" style="max-width: 900px; margin:auto;">
        <div class="row g-0">

            <!-- 🟣 Columna izquierda: datos -->
            <div class="col-md-6 p-3">
                <div class="mb-3">
                    <label for="txtId" class="form-label">ID</label>
                    <asp:TextBox runat="server" ID="txtId" CssClass="form-control" ReadOnly="true" />
                </div>

                <div class="mb-3">
                    <label for="txtNombre" class="form-label">Nombre</label>
                    <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" ReadOnly="true" />
                </div>

                <div class="mb-3">
                    <label for="txtCodigo" class="form-label">Código</label>
                    <asp:TextBox runat="server" ID="txtCodigo" CssClass="form-control" ReadOnly="true" />
                </div>

                <div class="mb-3">
                    <label class="form-label">Categoría</label>
                    <asp:TextBox runat="server" ID="txtCategoria" CssClass="form-control" ReadOnly="true" />
                </div>

                <div class="mb-3">
                    <label class="form-label">Marca</label>
                    <asp:TextBox runat="server" ID="txtMarca" CssClass="form-control" ReadOnly="true" />
                </div>

                <div class="mb-3">
                    <label class="form-label">Precio</label>
                    <asp:TextBox runat="server" ID="txtPrecio" CssClass="form-control" ReadOnly="true" />
                </div>

                <a href="ListaDeArticulos.aspx" class="btn btn-secondary mt-2">Volver</a>
            </div>

            <!-- 🟢 Columna derecha: carrusel -->
            <div class="col-md-6 p-3">
                <div id="carouselArticulo" class="carousel slide" data-bs-ride="carousel">
                    <div class="carousel-inner">
                        <asp:Repeater ID="repImagenes" runat="server">
                            <ItemTemplate>
                                <div class='carousel-item <%# Container.ItemIndex == 0 ? "active" : "" %>'>
                                    <img src='<%# Eval("ImagenUrl") %>' class="d-block w-100"
                                         style="height:300px; object-fit:contain;" alt="Imagen del artículo" />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                    <button class="carousel-control-prev" type="button" data-bs-target="#carouselArticulo" data-bs-slide="prev">
                        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                        <span class="visually-hidden">Anterior</span>
                    </button>
                    <button class="carousel-control-next" type="button" data-bs-target="#carouselArticulo" data-bs-slide="next">
                        <span class="carousel-control-next-icon" aria-hidden="true"></span>
                        <span class="visually-hidden">Siguiente</span>
                    </button>
                </div>
            </div>

        </div>
    </div>

</asp:Content>