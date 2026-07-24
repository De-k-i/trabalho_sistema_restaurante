using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabaio
{
    internal class ItemCardapio
    {
        public int Id { get; private set; }
        public string Nome { get; set; }
        public Categoria Categoria { get; set; }
        private decimal _precoBase;
        public decimal PrecoBase {
            get => _precoBase;
            private set
            {
                if (value <= 0) throw new ArgumentException("Preço deve ser maior que R$ 0,00.");
                _precoBase = value;
            }
        }
        public bool EstaDisponivel { get; private set; }

        public ItemCardapio(int id, string nome, Categoria categoria, decimal precoBase)
        {
            Id = id;
            Nome = nome;
            Categoria = categoria;
            PrecoBase = precoBase;
            EstaDisponivel = true;
        }

        public void PausarVendas()
        {
            EstaDisponivel = false;
        }

        public void ReativarVendas()
        {
            EstaDisponivel = true;
        }

        public decimal AplicarDesconto(decimal porcentagem)
        {
            if (porcentagem <= 0) throw new ArgumentOutOfRangeException("Desconto deve ser superior a 0%.");
            if (porcentagem > 30) throw new ArgumentOutOfRangeException("Desconto deve ser igual ou inferior a 30%.");
            return PrecoBase * (1 - porcentagem / 100);
        }

        public void AlterarPrecoBase(decimal novoPreco)
        {
            PrecoBase = novoPreco;
        }

        public override string ToString()
        {
            return $"{Id} - {Nome} - R$ {PrecoBase:N2} - {(EstaDisponivel ? "Disponível" : "Indisponível")}";
        }
    }
}
