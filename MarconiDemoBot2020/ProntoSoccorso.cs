namespace MarconiDemoBot2020
{
    class ProntoSoccorso
    {
        public string Nome { get; set; }

        public Attesa Attesa { get; set; }

        public ProntoSoccorso(string nome, Attesa attesa)
        {
            Nome = nome;
            Attesa = attesa;
        }
    }
}
