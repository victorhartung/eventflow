<span id="top"></span>
<h1 align="center">
  <img src="https://github.com/user-attachments/assets/ac0d04d7-bf31-4920-b13e-9effb6eb9a55" width="150" />
  <br> EvetFlow
</h1>

<div align="center">	
  O sistema de gestão da Event Flow foi desenvolvido com o intuito de otimizar o gerenciamento de eventos, permitindo maior controle sobre inscrições, consulta de endereços para eventos por meio da API ViaCep e comunicação com os participantes. Visando proporcionar uma experiência mais integrada e organizada, alinhando as operações internas às necessidades de organizadores e usuários.<br>  
  <br>  

 <h2>Pré requisitos:</h2>

 Para que o sistema funcione corretamente, é necessário instalar as aplicações listadas abaixo:
 
[![Git](https://img.shields.io/badge/Git-F05032.svg?style=for-the-badge&logo=git&logoColor=white)](https://git-scm.com)
[![.NET 8.0](https://img.shields.io/badge/.NET_8.0-512BD4.svg?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)
[![Visual Studio](https://img.shields.io/badge/Visual_Studio-683D87.svg?style=for-the-badge&)](https://visualstudio.microsoft.com/pt-br/downloads/)

</div>


## :pushpin: Clonar Repositório

Para clonar e executar o repositório, é necessário ter o Git instalado em seu computador ou utilizar o próprio Visual Studio.

Para clonar via Git, siga os passos na linha de comando:

```bash
# Clonar esse repositório
git clone https://github.com/victorhartung/eventflow.git

# Acessar o repositório
cd eventflow
```

Para clonar via Visual Studio, selecione a opção "clonar um repositório", insira o link do repositório e escolha o caminho para o projeto.

<p align="center">
<td><img src="https://github.com/user-attachments/assets/74732fd9-28cd-4572-b110-b09efbdf1c2b" width="700px"/></td>
<td><img src="https://github.com/user-attachments/assets/33cf7f78-f767-4b35-9fd2-77a123e2b35c" width="700px"/></td>
</p>

## :pushpin: Visual Studio

Após clonar o repositório, faça as configurações necessárias para o bom funcionamento do sistema.

### ● Criação do banco de dados:

Para criar o banco de dados:
- Selecione Exibir > Pesquisa de Objetos do SQL Server;
- Botão Direito em `Banco de Dados` > `Adicionar Novo Banco de Dados`;
- Renomeie o nome para `BDEventFlow`;

<table border=0 cellspacing=0 celspadding=0>
  <tr>
    <td><img src="https://github.com/user-attachments/assets/db56e15c-fbd2-4aa2-be8f-54daeee5d74b" width="420px"/></td>
    <td><img src="https://github.com/user-attachments/assets/c1d9aab0-29a5-4dc5-9068-82aa33f8fab5" width="420px"/></td>
  </tr>
  </table>  
<p align="center">
  <img src="https://github.com/user-attachments/assets/813254d4-3423-48a4-8c7f-86c26de0efaf" width="620px"/>
</p>

### ● Atualização do banco de dados:

- Selecione `Ferramentas` > `Gerenciador de Paotes do NuGet` > `Console de Gerenciador de Pacotes`;
- Insira o comando `Update-database` para subir as atualizações;
<p align="center">
  
</p>
<table border=0 cellspacing=0 celspadding=0>
  <tr>
    <td><img src="https://github.com/user-attachments/assets/fefe1165-ca5e-4f19-9973-176df637bb7c" width="420px"/></td>
    <td><img src="https://github.com/user-attachments/assets/e8a100bd-19e9-4b9b-ae32-91ac59079653" width="420px"/></td>
  </tr>
  </table> 

### ● Executar projeto:

Execute o projeto selecionando o `https` ou a tecla `F5`
<p align="center">
  <img src="https://github.com/user-attachments/assets/04d8ea74-7692-4a1e-8d12-5f16089e7704" width="620px"/>
</p>

<h2></h2>
