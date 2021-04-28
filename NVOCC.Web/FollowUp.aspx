<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="FollowUp.aspx.vb" Inherits="NVOCC.Web.FollowUp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
ul li { list-style:none;}
.linha-do-tempo {
position: relative;
max-width: 1024px;height: auto;
padding: 0px;margin: 0px auto;
overflow: hidden;
margin-bottom:5px
}
.marcador {
width: 50%;float: left;
height: 100%;position: absolute;
z-index: -1;border-right: 2px dashed rgb(0, 54, 99);box-sizing: border-box;} 
.Evento {
font-weight: 600;
font-size: 15px;
letter-spacing: 3px;
color: #f0f0f0;
background-color: rgb(231, 120, 23);
background-image: -webkit-linear-gradient(140deg,
rgba(255, 255, 255, .2) 50%,
transparent 50%,
transparent);
text-align: center;
margin-top:  7%;
margin: 1% auto;
clear: both;}
.item {
  width: 44%;
  float: right;}
.item:nth-of-type(2n) {
  float: left; 
}
.item p {
  color:rgb(66, 65, 60);
  text-align: center;}
.item p a {
  color: #e44b4b;
  text-align: center;}
.linha-do-tempo{
    padding:0px;
}
@media all and (max-width: 650px) {
.item {width: 80%;}}  

</style>

                     <div id="divConteudoDinamico"  runat="server">
        </div> 
                    

    <div id="TESTE" style="overflow:scroll" visible="false"  runat="server">
        
        </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
     <script src="Content/js/jquery.smartWizard.js"></script>
    <script src="Content/js/select2.min.js"></script>
</asp:Content>
