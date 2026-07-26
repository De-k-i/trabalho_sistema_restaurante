using System;

namespace Trabaio
{
    internal class ItemCardapio
    {
        private int _id;
        private string _nome;
        private decimal _precoBase;
        private Categoria _categoria;

        public int Id
        {
            get => _id;
            private set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException("ID deve ser maior que zero.", nameof(value));
                _id = value;
            }
        }

        public string Nome
        {
            get => _nome;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("O nome do item não pode ser vazio ou nulo.", nameof(value));
                _nome = value.Trim();
            }
        }

        public Categoria Categoria
        {
            get => _categoria;
            private set
            {
                if (!Enum.IsDefined(typeof(Categoria), value)) throw new ArgumentException("Categoria inválida.", nameof(value));
                _categoria =  value;
            }
        }

        public decimal PrecoBase
        {
            get => _precoBase;
            private set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Preço deve ser maior que R$ 0,00.");
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

        public void AlterarPrecoBase(decimal novoPreco)
        {
            PrecoBase = novoPreco;
        }

        public void AplicarDesconto(decimal porcentagem)
        {
            if (porcentagem <= 0)
                throw new ArgumentOutOfRangeException(nameof(porcentagem), "Desconto deve ser superior a 0%.");
            if (porcentagem > 30)
                throw new ArgumentOutOfRangeException(nameof(porcentagem),
                    "Desconto deve ser igual ou inferior a 30%.");

            PrecoBase = PrecoBase * (1 - porcentagem / 100);
        }

        public void AplicarAcrescimo(decimal porcentagem)
        {
            if (porcentagem <= 0)
                throw new ArgumentOutOfRangeException(nameof(porcentagem),
                    "A porcentagem de aumento deve ser maior que zero.");

            PrecoBase = PrecoBase * (1 + porcentagem / 100);
        }

        public override string ToString()
        {
            return
                $"[{Id}] {Nome} ({Categoria}) - {PrecoBase:C2} - STATUS: {(EstaDisponivel ? "Disponível" : "Pausado")}";
        }
    }
}