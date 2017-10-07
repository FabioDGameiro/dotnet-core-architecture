# CHANGELOG

## 0.8.0

- Adição do suporte a login externo com através do provider **Windows Authentication**
- Adição do suporte a login externo com através do provider **Facebook Authentication**

## 0.7.0

- Adição das entidades, contexto e repositórios no projeto `IdentityProvider` para trabalhar com usuários no banco de dados.
- Substituição da configuração `AddTestUsers(...)` para `AddUserStore()` para utilizar os usuários cadastrados no banco de dados
- Adição da funcionalidade **Registrar Usuário**. Sendo assim o Provider permite registrar novos usuários.

## 0.6.0

- Adição das configurações de **offline_access** para que a aplicação possa renovar os tokens de acesso através de um **refresh token** toda as vezes que o token de acesso expirar.
- Substituição do tipo de **Access Token** de *JWT Token* para *Reference Token*, tendo assim, maior controle sobre o tempo de expiração do token e possibilitando também a vantagem de poder revogar um Token.

## 0.5.0

- **Mvc Client** Redireciona para a tela de Acesso Negado quando o acesso à alguma página ou a alguma API não for concedido.
- Restringidos alguns métodos da API para algumas roles apenas.
- Atualização das autorizações baseadas em roles, substituição das **roles** por **policies**.

## 0.4.0

- Configuração da classe `AccountOptions` para realizar Logout automaticamente, sem a necessidade de clicar no link para redirecionar de volta para a aplicação cliente.

## 0.3.0

- Integração dos Clientes `MVC` e `JavaScript` com a API de Recursos (protegida). 
- Adição do scope "address", assim o cliente `MVC` pode ler o endereço das Claims do Usuário logado.
- Adição da seção `GetUserInfo` no cliente `MVC` para realizar a chamada manual do endpoint `GetUserInfo` e retornar todas as clains do usuário logado para a página.

## 0.2.0

- Adição do cliente `taskmvc` na classe `Config.js` do projeto `IdentityProvider`. Configuração exclusiva para o cliente `MvcApp`, utilizando o tipo de credencial `HybridAndClientCredentials`
- Configuração do cliente `MvcApp` para ser autenticado através do `IdentityProvider`

## 0.1.0

- Adição do projeto `Project.IdentityProvider.Api` e configuração do IdentityServer no projeto
- Adição do projeto `Project.Resources.Api`
- Adição do projeto `Project.MvcApp`