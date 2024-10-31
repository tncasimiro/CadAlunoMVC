using System;

namespace CadAlunoMVC.Models
{
    public class AlunoViewModel : PadraoViewModel
    {        
        public string Nome { get; set; }
        public double? Mensalidade { get; set; }
        public int CidadeId { get; set; }
        public DateTime DataNascimento { get; set; }

        //campos que não existem na tabela aluno, são produto de join
        public string NomeCidade { get; set; }
    }

}
