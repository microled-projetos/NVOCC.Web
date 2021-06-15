<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Conferencia.aspx.vb" Inherits="NVOCC.Web.Conferencia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
            <div class="row principal">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">CONFERÊNCIA
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active">
                            <a href="#Master" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Master
                            </a>
                        </li>
                        <li>
                            <a href="#House" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>House
                            </a>
                        </li>
                    </ul>

                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="Master">
                            <br />
                            <div class="row">

                                       <div class="col-sm-2">
                                    <div class="form-group">
                                        <strong>MBL:</strong>&nbsp;<asp:Label ID="lblImportador_FCL"  runat="server"/>
                                        </div>
                                           </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <strong>TIPO FRETE:</strong>&nbsp;<asp:Label ID="Label1"  runat="server"/>
                                        </div>
                                           </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <strong>EMBARQUE:</strong>&nbsp;<asp:Label ID="Label2"  runat="server"/>
                                        </div>
                                           </div>

                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <strong>SEM DEVOLUÇÃO DO FRETE:</strong>&nbsp;
                                        </div>
                                           </div>
                                </div>
                             <div class="row">
                                       <div class="col-sm-2">
                                    <div class="form-group">
                                        <strong>MOEDA:</strong>&nbsp;<asp:Label ID="Label3"  runat="server"/>
                                        </div>
                                           </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <strong>TIPO ESTUFAGEM:</strong>&nbsp;<asp:Label ID="Label4"  runat="server"/>
                                        </div>
                                           </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <strong>CHEGADA:</strong>&nbsp;<asp:Label ID="Label5"  runat="server"/>
                                        </div>
                                           </div>

                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <strong>FREE HAND:</strong>&nbsp;<asp:Label ID="Label6"  runat="server"/>
                                        </div>
                                           </div>
                                </div>
                             <div class="row">
                                       <div class="col-sm-8">
                                    <div class="form-group">
                                      grid devolucao frete
                                        </div>
                                           </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                       grid comissoes
                                        </div>
                                           </div>
                                
                                </div>
                            <div class="row">
                                       <div class="col-sm-12">
                                    <div class="form-group">
                                      grid junção das taxas
                                        </div>
                                           </div>
                                
                                </div>
                            <div class="row">
                                       <div class="col-sm-12">
                                    <div class="form-group">
                                      grid itens invoice
                                        </div>
                                           </div>
                               
                                
                                </div>
                        </div>




                        <div class="tab-pane fade" id="House">
                            <br />
                             <div class="row">
                                                                         <div class="linha-colorida">MASTER</div>

                                       <div class="col-sm-2">
                                    <div class="form-group">
                                        <strong>MBL:</strong>&nbsp;<asp:Label ID="Label7"  runat="server"/>
                                        </div>
                                           </div>
                                 <div class="col-sm-2">
                                    <div class="form-group">
                                        <strong>MOEDA:</strong>&nbsp;<asp:Label ID="Label18"  runat="server"/>
                                        </div>
                                           </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <strong>TIPO FRETE:</strong>&nbsp;<asp:Label ID="Label8"  runat="server"/>
                                        </div>
                                           </div>
                                 <div class="col-sm-2">
                                    <div class="form-group">
                                        <strong>ESTUFAGEM:</strong>&nbsp;<asp:Label ID="Label14"  runat="server"/>
                                        </div>
                                           </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <strong>EMBARQUE:</strong>&nbsp;<asp:Label ID="Label9"  runat="server"/>
                                        </div>
                                           </div>

                                <div class="col-sm-2">
                                    <div class="form-group">
                                          <strong>CHEGADA:</strong>&nbsp;<asp:Label ID="Label15"  runat="server"/>
                                        </div>
                                           </div>
                                </div>
                             <div class="row">
                             <div class="linha-colorida">HOUSE</div>
                                 <div class="col-sm-2">
                                    <div class="form-group">
                                        <strong>HBL:</strong>&nbsp;<asp:Label ID="Label16"  runat="server"/>
                                        </div>
                                           </div>
                                 <div class="col-sm-2">
                                    <div class="form-group">
                                        <strong>PROCESSO:</strong>&nbsp;<asp:Label ID="Label17"  runat="server"/>
                                        </div>
                                           </div>
                                      
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <strong>TIPO FRETE:</strong>&nbsp;<asp:Label ID="Label11"  runat="server"/>
                                        </div>
                                           </div>
                                 <div class="col-sm-2">
                                    <div class="form-group">
                                        <strong>ESTUFAGEM:</strong>&nbsp;<asp:Label ID="Label10"  runat="server"/>
                                        </div>
                                           </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <strong>PESO BRUTO:</strong>&nbsp;<asp:Label ID="Label12"  runat="server"/>
                                        </div>
                                           </div>

                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <strong>PESO CUBICO:</strong>&nbsp;<asp:Label ID="Label13"  runat="server"/>
                                        </div>
                                           </div>
                                </div>
                            
                            <div class="row">
                                       <div class="col-sm-12">
                                    <div class="form-group">
                                      grid junção das taxas
                                        </div>
                                           </div>
                                
                                </div>
                            <div class="row">
                                       <div class="col-sm-12">
                                    <div class="form-group">
                                      grid itens invoice
                                        </div>
                                           </div>
                               
                                
                                </div>
                        </div>
                    </div>



                </div>
            </div>
        </div>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
