using CadAlunoMVC.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;

namespace CadAlunoMVC.DAO
{
    public class AlunoDAO : PadraoDAO<AlunoViewModel>
    {
        protected override SqlParameter[] CriaParametros(AlunoViewModel aluno)
        {
            SqlParameter[] parametros = new SqlParameter[5];
            parametros[0] = new SqlParameter("id", aluno.Id);
            parametros[1] = new SqlParameter("nome", aluno.Nome);
            if (aluno.Mensalidade == null)
                parametros[2] = new SqlParameter("mensalidade", DBNull.Value);
            else
                parametros[2] = new SqlParameter("mensalidade", aluno.Mensalidade);

            parametros[3] = new SqlParameter("cidadeId", aluno.CidadeId);
            parametros[4] = new SqlParameter("dataNascimento", aluno.DataNascimento);
            return parametros;
        }

        protected override AlunoViewModel MontaModel(DataRow registro)
        {
            AlunoViewModel a = new AlunoViewModel();
            a.Id = Convert.ToInt32(registro["id"]);
            a.Nome = registro["nome"].ToString();
            a.CidadeId = Convert.ToInt32(registro["cidadeId"]);
            a.DataNascimento = Convert.ToDateTime(registro["dataNascimento"]);
            if (registro["mensalidade"] != DBNull.Value)
                a.Mensalidade = Convert.ToDouble(registro["mensalidade"]);

            if (registro.Table.Columns.Contains("NomeCidade"))
                a.NomeCidade = registro["NomeCidade"].ToString();

            return a;
        }

        protected override void SetTabela()
        {
            Tabela = "Alunos";
            NomeSpListagem = "spListagemAlunos";
        }
    }
}



/*
 * 
 * ref. pagina 55 da apostila, que está com erro.
ALTER procedure [dbo].[spListagemAlunos]  
(
 @tabela varchar(max),
 @ordem varchar(max)
)
as
begin
	select Alunos.*, Cidades.Nome as NomeCidade  
	from alunos  
	Left join cidades on alunos.CidadeId = cidades.id  
	order by nome
end
  
 * 
 * 
 * */
