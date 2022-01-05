<%@ Page Title="FCA-Log - Agenciamento" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="NVOCC.Web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="float:right; display:none" > <a id="ajuda" href="#" title="Ajuda" ><svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" class="bi bi-question-circle-fill" viewBox="0 0 16 16">
  <path d="M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0zM5.496 6.033h.825c.138 0 .248-.113.266-.25.09-.656.54-1.134 1.342-1.134.686 0 1.314.343 1.314 1.168 0 .635-.374.927-.965 1.371-.673.489-1.206 1.06-1.168 1.987l.003.217a.25.25 0 0 0 .25.246h.811a.25.25 0 0 0 .25-.25v-.105c0-.718.273-.927 1.01-1.486.609-.463 1.244-.977 1.244-2.056 0-1.511-1.276-2.241-2.673-2.241-1.267 0-2.655.59-2.75 2.286a.237.237 0 0 0 .241.247zm2.325 6.443c.61 0 1.029-.394 1.029-.927 0-.552-.42-.94-1.029-.94-.584 0-1.009.388-1.009.94 0 .533.425.927 1.01.927z"/>
</svg></a></div>


    <div class="modal fade" id="modal-ajuda">
   <div class="modal-dialog modal-xl">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title">Sobre NVOCC:</h4>
            </div>
            <div class="modal-body">
                <strong>Objetivo:</strong> Agenciamento de cargas<br/><br/>

Passo 1: Cadastrar Parceiros CadastrarParceiros.aspx <br/>
nessa tela é possivel cadastrar parceiros clientes,armadores,agentes,importadores e etc...<br/><br/>

Passo 2: Cadastrar taxas dos parceiros clientes e armadores<br/>
Na tela TaxasLocaisArmador.aspx é possivel cadastrar taxas locais de parceiros armadores<br/>
Na tela TaxaParceiro.aspx é possivel cadastrar taxas de parceiros clientes<br/><br/>

Passo 3: Cadastrar uma tabela de frete<br/>
Nessa tela é possivel cadastrar uma tabela de frete para armadores, informando assim origem, destino,taxas e containers <br/><br/>

Passo 4: Cadastrar uma cotação<br/>
Nessa tela é posssivel cadastrar uma cotação para cliente, devido aos passos anteriores é possivel importar tanto taxas de clientes e armadores como tabelas de frete.<br/><br/>

Passso 5: Calculo, Impressao e Aprovação<br/>
Apos criaçao da cotação a mesma precisa passar pelo calculo, para assim ser impressa e enviada para o cliente para aprovação.<br/>
Ao ser aprava a cotação dá origem a um processo HBL<br/><br/>

Passo 6: Criar MBL<br/>
Agora que temos um HBL gerado de uma cotação precisamos gerar um MBL para este, e se for o caso, outros HBL.<br/>
Clicando neste botao é possivel abrir a tela para fazer um novo cadastro de MBL herdando algumas informações do HBL.<br/><br/>

Passo 7: Criar containers MBL <br/>
Apos a geração do MBL é necessario cadastrar para este a um ou mais containers<br/>


Passo 8: Vincular HBL <br/>
Apos o cadastro dos containers do MBL é necessario criar suas taxas.<br/><br/>


Passo 9: Vincular HBL <br/>
Apos criar as taxas do MBL é necessario vincular este a um ou mais HBLs.<br/><br/>

Passo 10: Calcular Processos<br/>
Apos conclusao de cadastro é necessario realizar o calculo do HBL.<br/><br/>

Passo 11: Financeiro<br/>
Tendo sua data de chegada preenchida, suas taxas calculadas e vinculo com MBL o processo já pode ser visualizado na tela do financeiro<br/><br/>

Passo 12: Solicitação de Pagamento<br/><br/>

Passo 13: Montagem de Pagamento<br/><br/>

Passo 14: Baixar ou Cancelar Pagamento<br/><br/>

Passo 15: Calcular Recebimento<br/><br/>

Passo 16: Emitir ND<br/><br/>

Passo 17: Baixar ou Cancelar Recebimento<br/><br/>

Passo 18: Enviar Para o Faturamento<br/><br/>

            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>
            </div>
        </div>
    </div>
</div>
            <script src="Content/js/jquery.min.js"></script>

    <script>

        $('#ajuda').on("click", function () {
            $('#modal-ajuda').modal('show');
        });
        </script>
</asp:Content>
