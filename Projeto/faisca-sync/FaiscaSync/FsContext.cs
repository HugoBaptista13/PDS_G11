using System;
using System.Collections.Generic;
using FaiscaSync.Models;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync;

public partial class FsContext : DbContext
{
    public FsContext()
    {
    }

    public FsContext(DbContextOptions<FsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aquisicao> Aquisicaos { get; set; }

    public virtual DbSet<Cargo> Cargos { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<CoberturaGarantia> CoberturaGarantia { get; set; }

    public virtual DbSet<Contato> Contatos { get; set; }

    public virtual DbSet<Cpostal> Cpostals { get; set; }

    public virtual DbSet<EstadoGarantia> EstadoGarantia { get; set; }

    public virtual DbSet<EstadoManutencao> EstadoManutencaos { get; set; }

    public virtual DbSet<EstadoVeiculo> EstadoVeiculos { get; set; }

    public virtual DbSet<Fatura> Faturas { get; set; }

    public virtual DbSet<Funcionario> Funcionarios { get; set; }

    public virtual DbSet<Garantia> Garantia { get; set; }

    public virtual DbSet<HistoricoEstadoVeiculo> HistoricoEstadoVeiculos { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<Manutencao> Manutencaos { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Modelo> Modelos { get; set; }

    public virtual DbSet<Morada> Morada { get; set; }

    public virtual DbSet<Motor> Motors { get; set; }

    public virtual DbSet<Pagamento> Pagamentos { get; set; }

    public virtual DbSet<PosVenda> PosVenda { get; set; }

    public virtual DbSet<TipoContato> TipoContatos { get; set; }

    public virtual DbSet<TipoPagamento> TipoPagamentos { get; set; }

    public virtual DbSet<TipoVeiculo> TipoVeiculos { get; set; }

    public virtual DbSet<Veiculo> Veiculos { get; set; }

    public virtual DbSet<Venda> Vendas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-0HDG436U;Initial Catalog=fs;Integrated Security=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aquisicao>(entity =>
        {
            entity.HasKey(e => e.IdAquisicao).HasName("PK__Aquisica__4D69D7145B3496AA");

            entity.ToTable("Aquisicao");

            entity.Property(e => e.IdAquisicao).HasColumnName("ID_Aquisicao");
            entity.Property(e => e.Dataaquisicao)
                .HasColumnType("datetime")
                .HasColumnName("dataaquisicao");
            entity.Property(e => e.Fornecedor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("fornecedor");
            entity.Property(e => e.Origemveiculo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("origemveiculo");
            entity.Property(e => e.Valorpago)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("valorpago");
        });

        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(e => e.IdCargo).HasName("PK__Cargo__8D69B95FBFD401C2");

            entity.ToTable("Cargo");

            entity.Property(e => e.IdCargo).HasColumnName("ID_Cargo");
            entity.Property(e => e.Nomecargo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nomecargo");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Cliente__E005FBFF4CBADA43");

            entity.ToTable("Cliente");

            entity.Property(e => e.IdCliente).HasColumnName("ID_Cliente");
            entity.Property(e => e.Datanasc)
                .HasColumnType("datetime")
                .HasColumnName("datanasc");
            entity.Property(e => e.IdMorada).HasColumnName("ID_Morada");
            entity.Property(e => e.Nif)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("nif");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nome");

            entity.HasOne(d => d.IdMoradaNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdMorada)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKCliente283816");
        });

        modelBuilder.Entity<CoberturaGarantia>(entity =>
        {
            entity.HasKey(e => e.IdCoberturaGarantia).HasName("PK__Cobertur__B7834C80C4D8EB09");

            entity.Property(e => e.IdCoberturaGarantia).HasColumnName("ID_CoberturaGarantia");
            entity.Property(e => e.IdGarantia).HasColumnName("ID_Garantia");
            entity.Property(e => e.IdManutencao).HasColumnName("ID_Manutencao");

            entity.HasOne(d => d.IdGarantiaNavigation).WithMany(p => p.CoberturaGarantia)
                .HasForeignKey(d => d.IdGarantia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKCoberturaG513105");

            entity.HasOne(d => d.IdManutencaoNavigation).WithMany(p => p.CoberturaGarantia)
                .HasForeignKey(d => d.IdManutencao)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKCoberturaG857106");
        });

        modelBuilder.Entity<Contato>(entity =>
        {
            entity.HasKey(e => e.Idcontato).HasName("PK__Contato__6A511EED952B4C4A");

            entity.ToTable("Contato");

            entity.Property(e => e.Idcontato).HasColumnName("IDContato");
            entity.Property(e => e.Detalhescontato)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("detalhescontato");
            entity.Property(e => e.IdCliente).HasColumnName("ID_Cliente");
            entity.Property(e => e.IdFuncionario).HasColumnName("ID_Funcionario");
            entity.Property(e => e.IdTipoContato).HasColumnName("ID_TipoContato");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Contatos)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKContato391066");

            entity.HasOne(d => d.IdFuncionarioNavigation).WithMany(p => p.Contatos)
                .HasForeignKey(d => d.IdFuncionario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKContato316649");

            entity.HasOne(d => d.IdTipoContatoNavigation).WithMany(p => p.Contatos)
                .HasForeignKey(d => d.IdTipoContato)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKContato996518");
        });

        modelBuilder.Entity<Cpostal>(entity =>
        {
            entity.HasKey(e => e.IdCpostal).HasName("PK__CPostal__D39F2F58A1E01592");

            entity.ToTable("CPostal");

            entity.Property(e => e.IdCpostal).HasColumnName("ID_CPostal");
            entity.Property(e => e.Localidade)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("localidade");
        });

        modelBuilder.Entity<EstadoGarantia>(entity =>
        {
            entity.HasKey(e => e.IdEstadoGarantia).HasName("PK__EstadoGa__E954DE58DCC7AFC8");

            entity.Property(e => e.IdEstadoGarantia).HasColumnName("ID_EstadoGarantia");
            entity.Property(e => e.Descricaoestadogarantia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricaoestadogarantia");
        });

        modelBuilder.Entity<EstadoManutencao>(entity =>
        {
            entity.HasKey(e => e.IdEstadoManutencao).HasName("PK__EstadoMa__4497AA230ADFA3A0");

            entity.ToTable("EstadoManutencao");

            entity.Property(e => e.IdEstadoManutencao).HasColumnName("ID_EstadoManutencao");
            entity.Property(e => e.Descricaoestadomanutencao)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricaoestadomanutencao");
        });

        modelBuilder.Entity<EstadoVeiculo>(entity =>
        {
            entity.HasKey(e => e.IdEstadoVeiculo).HasName("PK__EstadoVe__450DCABDD014858B");

            entity.ToTable("EstadoVeiculo");

            entity.Property(e => e.IdEstadoVeiculo).HasColumnName("ID_EstadoVeiculo");
            entity.Property(e => e.Descricaoestadoveiculo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricaoestadoveiculo");
        });

        modelBuilder.Entity<Fatura>(entity =>
        {
            entity.HasKey(e => e.IdFatura).HasName("PK__Fatura__9F2CBCBD3952F6E1");

            entity.ToTable("Fatura");

            entity.Property(e => e.IdFatura).HasColumnName("ID_Fatura");
            entity.Property(e => e.Dataemissao)
                .HasColumnType("datetime")
                .HasColumnName("dataemissao");
            entity.Property(e => e.IdVendas).HasColumnName("ID_Vendas");
            entity.Property(e => e.Valorfatura)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("valorfatura");

            entity.HasOne(d => d.IdVendasNavigation).WithMany(p => p.Faturas)
                .HasForeignKey(d => d.IdVendas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKFatura169649");
        });

        modelBuilder.Entity<Funcionario>(entity =>
        {
            entity.HasKey(e => e.IdFuncionario).HasName("PK__Funciona__0AE977B9C7794793");

            entity.ToTable("Funcionario");

            entity.Property(e => e.IdFuncionario).HasColumnName("ID_Funcionario");
            entity.Property(e => e.Datacontratacao)
                .HasColumnType("datetime")
                .HasColumnName("datacontratacao");
            entity.Property(e => e.Datanascimento)
                .HasColumnType("datetime")
                .HasColumnName("datanascimento");
            entity.Property(e => e.IdCargo).HasColumnName("ID_Cargo");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nome");

            entity.HasOne(d => d.IdCargoNavigation).WithMany(p => p.Funcionarios)
                .HasForeignKey(d => d.IdCargo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKFuncionari143762");
        });

        modelBuilder.Entity<Garantia>(entity =>
        {
            entity.HasKey(e => e.IdGarantia).HasName("PK__Garantia__E1CE1E9ECD581535");

            entity.Property(e => e.IdGarantia).HasColumnName("ID_Garantia");
            entity.Property(e => e.Datafim)
                .HasColumnType("datetime")
                .HasColumnName("datafim");
            entity.Property(e => e.Datainicio)
                .HasColumnType("datetime")
                .HasColumnName("datainicio");
            entity.Property(e => e.IdEstadoGarantia).HasColumnName("ID_EstadoGarantia");

            entity.HasOne(d => d.IdEstadoGarantiaNavigation).WithMany(p => p.Garantia)
                .HasForeignKey(d => d.IdEstadoGarantia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKGarantia947345");
        });

        modelBuilder.Entity<HistoricoEstadoVeiculo>(entity =>
        {
            entity.HasKey(e => e.IdHistEstadoVeiculo).HasName("PK__Historic__714F4699C42EAEBD");

            entity.ToTable("HistoricoEstadoVeiculo");

            entity.Property(e => e.IdHistEstadoVeiculo).HasColumnName("ID_HistEstadoVeiculo");
            entity.Property(e => e.Dataalteracao)
                .HasColumnType("datetime")
                .HasColumnName("dataalteracao");
            entity.Property(e => e.IdEstadoVeiculo).HasColumnName("ID_EstadoVeiculo");
            entity.Property(e => e.IdFuncionario).HasColumnName("ID_Funcionario");
            entity.Property(e => e.IdVeiculo).HasColumnName("ID_Veiculo");

            entity.HasOne(d => d.IdEstadoVeiculoNavigation).WithMany(p => p.HistoricoEstadoVeiculos)
                .HasForeignKey(d => d.IdEstadoVeiculo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKHistoricoE271913");

            entity.HasOne(d => d.IdFuncionarioNavigation).WithMany(p => p.HistoricoEstadoVeiculos)
                .HasForeignKey(d => d.IdFuncionario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKHistoricoE997461");

            entity.HasOne(d => d.IdVeiculoNavigation).WithMany(p => p.HistoricoEstadoVeiculos)
                .HasForeignKey(d => d.IdVeiculo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKHistoricoE623897");
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.IdLogin).HasName("PK__Login__2C3E3A9FED327569");

            entity.ToTable("Login");

            entity.Property(e => e.IdLogin).HasColumnName("ID_Login");
            entity.Property(e => e.IdFuncionario).HasColumnName("ID_Funcionario");
            entity.Property(e => e.Password)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.IdFuncionarioNavigation).WithMany(p => p.Logins)
                .HasForeignKey(d => d.IdFuncionario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKLogin68910");
        });

        modelBuilder.Entity<Manutencao>(entity =>
        {
            entity.HasKey(e => e.IdManutencao).HasName("PK__Manutenc__D49EF33E541F60CF");

            entity.ToTable("Manutencao");

            entity.Property(e => e.IdManutencao).HasColumnName("ID_Manutencao");
            entity.Property(e => e.Custo)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("custo");
            entity.Property(e => e.Datamanutencao)
                .HasColumnType("datetime")
                .HasColumnName("datamanutencao");
            entity.Property(e => e.Descricaomanutencao).HasColumnName("descricaomanutencao");
            entity.Property(e => e.FaturaIdFatura).HasColumnName("FaturaID_Fatura");
            entity.Property(e => e.IdEstadoManutencao).HasColumnName("ID_EstadoManutencao");
            entity.Property(e => e.IdPosVenda).HasColumnName("ID_PosVenda");
            entity.Property(e => e.IdVeiculo).HasColumnName("ID_Veiculo");
            entity.Property(e => e.Tipomanutencao)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipomanutencao");

            entity.HasOne(d => d.FaturaIdFaturaNavigation).WithMany(p => p.Manutencaos)
                .HasForeignKey(d => d.FaturaIdFatura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKManutencao841433");

            entity.HasOne(d => d.IdEstadoManutencaoNavigation).WithMany(p => p.Manutencaos)
                .HasForeignKey(d => d.IdEstadoManutencao)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKManutencao249943");

            entity.HasOne(d => d.IdPosVendaNavigation).WithMany(p => p.Manutencaos)
                .HasForeignKey(d => d.IdPosVenda)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKManutencao585673");

            entity.HasOne(d => d.IdVeiculoNavigation).WithMany(p => p.Manutencaos)
                .HasForeignKey(d => d.IdVeiculo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKManutencao977192");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.IdMarca).HasName("PK__Marca__9B8F8DB211E2447F");

            entity.ToTable("Marca");

            entity.Property(e => e.IdMarca).HasColumnName("ID_Marca");
            entity.Property(e => e.Descricaomarca)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricaomarca");
        });

        modelBuilder.Entity<Modelo>(entity =>
        {
            entity.HasKey(e => e.IdModelo).HasName("PK__Modelo__813C2372D98AC9C0");

            entity.ToTable("Modelo");

            entity.Property(e => e.IdModelo).HasColumnName("ID_Modelo");
            entity.Property(e => e.IdMarca).HasColumnName("ID_Marca");
            entity.Property(e => e.Nomemodelo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nomemodelo");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Modelos)
                .HasForeignKey(d => d.IdMarca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKModelo635512");
        });

        modelBuilder.Entity<Morada>(entity =>
        {
            entity.HasKey(e => e.IdMorada).HasName("PK__Morada__67BC26C81853F744");

            entity.Property(e => e.IdMorada).HasColumnName("ID_Morada");
            entity.Property(e => e.Descricaomorada)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricaomorada");
            entity.Property(e => e.IdCpostal).HasColumnName("ID_CPostal");
            entity.Property(e => e.Numero)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("numero");
            entity.Property(e => e.Pais)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("pais");
            entity.Property(e => e.Rua)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("rua");

            entity.HasOne(d => d.IdCpostalNavigation).WithMany(d => d.Morada)
                .HasForeignKey(d => d.IdCpostal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKMorada112888");
        });

        modelBuilder.Entity<Motor>(entity =>
        {
            entity.HasKey(e => e.IdMotor).HasName("PK__Motor__D556B4EA2A5F354E");

            entity.ToTable("Motor");

            entity.Property(e => e.IdMotor).HasColumnName("ID_Motor");
            entity.Property(e => e.Combustivel)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("combustivel");
            entity.Property(e => e.Potencia)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("potencia");
            entity.Property(e => e.Tipomotor)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipomotor");
        });

        modelBuilder.Entity<Pagamento>(entity =>
        {
            entity.HasKey(e => e.IdPagamento).HasName("PK__Pagament__B25E7A2209CD042E");

            entity.ToTable("Pagamento");

            entity.Property(e => e.IdPagamento).HasColumnName("ID_Pagamento");
            entity.Property(e => e.Datapagamento)
                .HasColumnType("datetime")
                .HasColumnName("datapagamento");
            entity.Property(e => e.IdFatura).HasColumnName("ID_Fatura");
            entity.Property(e => e.IdTipoPagamento).HasColumnName("ID_TipoPagamento");
            entity.Property(e => e.Valorpagamento)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("valorpagamento");

            entity.HasOne(d => d.IdFaturaNavigation).WithMany(p => p.Pagamentos)
                .HasForeignKey(d => d.IdFatura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKPagamento97648");

            entity.HasOne(d => d.IdTipoPagamentoNavigation).WithMany(p => p.Pagamentos)
                .HasForeignKey(d => d.IdTipoPagamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKPagamento28267");
        });

        modelBuilder.Entity<PosVenda>(entity =>
        {
            entity.HasKey(e => e.IdPosVenda).HasName("PK__PosVenda__4D132E8A6D4912BA");

            entity.Property(e => e.IdPosVenda).HasColumnName("ID_PosVenda");
            entity.Property(e => e.IdGarantia).HasColumnName("ID_Garantia");
            entity.Property(e => e.IdVendas).HasColumnName("ID_Vendas");

            entity.HasOne(d => d.IdGarantiaNavigation).WithMany(p => p.PosVenda)
                .HasForeignKey(d => d.IdGarantia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKPosVenda296319");

            entity.HasOne(d => d.IdVendasNavigation).WithMany(p => p.PosVenda)
                .HasForeignKey(d => d.IdVendas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKPosVenda565800");
        });

        modelBuilder.Entity<TipoContato>(entity =>
        {
            entity.HasKey(e => e.IdTipoContato).HasName("PK__TipoCont__5B2B3BA5F26F8F7D");

            entity.ToTable("TipoContato");

            entity.Property(e => e.IdTipoContato).HasColumnName("ID_TipoContato");
            entity.Property(e => e.Descricaotipocontato)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricaotipocontato");
        });

        modelBuilder.Entity<TipoPagamento>(entity =>
        {
            entity.HasKey(e => e.IdTipoPagamento).HasName("PK__TipoPaga__16A41D5C3F18763F");

            entity.ToTable("TipoPagamento");

            entity.Property(e => e.IdTipoPagamento).HasColumnName("ID_TipoPagamento");
            entity.Property(e => e.Descricaotipopagamento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricaotipopagamento");
        });

        modelBuilder.Entity<TipoVeiculo>(entity =>
        {
            entity.HasKey(e => e.IdTipoVeiculo).HasName("PK__TipoVeic__E6BC50A198CA4A62");

            entity.ToTable("TipoVeiculo");

            entity.Property(e => e.IdTipoVeiculo).HasColumnName("ID_TipoVeiculo");
            entity.Property(e => e.Descricaotveiculo)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("descricaotveiculo");
        });

        modelBuilder.Entity<Veiculo>(entity =>
        {
            entity.HasKey(e => e.IdVeiculo).HasName("PK__Veiculo__808FFECF634F2C7E");

            entity.ToTable("Veiculo");

            entity.Property(e => e.IdVeiculo).HasColumnName("ID_Veiculo");
            entity.Property(e => e.Anofabrico).HasColumnName("anofabrico");
            entity.Property(e => e.Chassi)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("chassi");
            entity.Property(e => e.Cor)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("cor");
            entity.Property(e => e.IdAquisicao).HasColumnName("ID_Aquisicao");
            entity.Property(e => e.IdEstadoVeiculo).HasColumnName("ID_EstadoVeiculo");
            entity.Property(e => e.IdModelo).HasColumnName("ID_Modelo");
            entity.Property(e => e.IdMotor).HasColumnName("ID_Motor");
            entity.Property(e => e.IdTipoVeiculo).HasColumnName("ID_TipoVeiculo");
            entity.Property(e => e.Matricula)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("matricula");
            entity.Property(e => e.Preco)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("preco");
            entity.Property(e => e.Quilometros)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("quilometros");

            entity.HasOne(d => d.IdAquisicaoNavigation).WithMany(p => p.Veiculos)
                .HasForeignKey(d => d.IdAquisicao)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKVeiculo220729");

            entity.HasOne(d => d.IdEstadoVeiculoNavigation).WithMany(p => p.Veiculos)
                .HasForeignKey(d => d.IdEstadoVeiculo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKVeiculo261358");

            entity.HasOne(d => d.IdModeloNavigation).WithMany(p => p.Veiculos)
                .HasForeignKey(d => d.IdModelo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKVeiculo287998");

            entity.HasOne(d => d.IdMotorNavigation).WithMany(p => p.Veiculos)
                .HasForeignKey(d => d.IdMotor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKVeiculo342687");

            entity.HasOne(d => d.IdTipoVeiculoNavigation).WithMany(p => p.Veiculos)
                .HasForeignKey(d => d.IdTipoVeiculo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKVeiculo810702");
        });

        modelBuilder.Entity<Venda>(entity =>
        {
            entity.HasKey(e => e.IdVendas).HasName("PK__Vendas__9DE9C8701E360073");

            entity.Property(e => e.IdVendas).HasColumnName("ID_Vendas");
            entity.Property(e => e.Datavenda)
                .HasColumnType("datetime")
                .HasColumnName("datavenda");
            entity.Property(e => e.IdCliente).HasColumnName("ID_Cliente");
            entity.Property(e => e.IdFuncionario).HasColumnName("ID_Funcionario");
            entity.Property(e => e.IdVeiculo).HasColumnName("ID_Veiculo");
            entity.Property(e => e.Valorvenda).HasColumnName("valorvenda");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Venda)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKVendas812518");

            entity.HasOne(d => d.IdFuncionarioNavigation).WithMany(p => p.Venda)
                .HasForeignKey(d => d.IdFuncionario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKVendas738101");

            entity.HasOne(d => d.IdVeiculoNavigation).WithMany(p => p.Venda)
                .HasForeignKey(d => d.IdVeiculo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKVendas888333");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
