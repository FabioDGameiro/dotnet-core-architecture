# CHANGELOG

## 0.3.0 (2017-08-21)

- Configura a aplicação para retornar um Status Code **406 - NOT ACCEPTABLE** para outros formatos de respostas diferentes dos aceitados.

## 0.2.0 (2017-08-20)

- Adição das classes DTOs `UsuarioGetModel` e `UsuarioEnderecoGetModel` para formatar o retornos dos métodos `GET`
- Adição da biblioteca `AutoMapper` para auxiliar o mapeamento das entidades para as DTOs.

## 0.1.0 (2017-08-19)

- Adição da estrutura inicial contento as entidades `Usuario` e `UsuarioEndereco`, contextos e repositórios com dados iniciais para testes.
- Adição dos controllers `UsuarioController` e `UsuarioEnderecoController`