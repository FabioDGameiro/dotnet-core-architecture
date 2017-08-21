﻿using Domain.Usuarios;
using System;
using System.Collections.Generic;
using UsuariosAPI.Models.Usuarios.Endereco;

namespace UsuariosAPI.Models.Usuarios
{
    //
    // Classe de modelo para o retorno do método GET da API
    // 
    // A classe de retorno pode retornar informações formatadas da entidade principal, 
    // como por exemplo a "Idade" do usuário, que é resultado de um cálculo feito a partir da "Data de Nascimento" do usuário.
    // Outro exemplo é o "Sexo" do usuário, que pode ser formatado, a partir do enum SexoType da entidade.
    //

    public class UsuarioGetModel
    {
        public Guid Id { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public int Idade { get; set; }
        public string Sexo { get; set; }
    }
}
