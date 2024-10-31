using CadAlunoMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CadAlunoMVC.DAO
{
    public class CidadeDAO : PadraoDAO<CidadeViewModel>
    {
        protected override SqlParameter[] CriaParametros(CidadeViewModel model)
        {
            SqlParameter[] p = new SqlParameter[]
            {
                new SqlParameter("id", model.Id),
                new SqlParameter("nome", model.Nome)
            };
            return p;            
        }

        protected override CidadeViewModel MontaModel(DataRow registro)
        {
            CidadeViewModel c = new CidadeViewModel()
            {
                Id = Convert.ToInt32(registro["id"]),
                Nome = registro["nome"].ToString()
            };
            return c;
        }

        protected override void SetTabela()
        {
            Tabela = "cidades";
        }
    }
}


/*
 
create procedure spInsert_Cidades
(
 @id int,
 @nome varchar(max) 
)
as
begin
 insert into Cidades 
 (id, nome)
 values
 (@id, @nome)
end

GO
create procedure spUpdate_Cidades
(
 @id int,
 @nome varchar(max)
)
as
begin
 update cidades set 
 nome = @nome  
 where id = @id 
end
GO
 */