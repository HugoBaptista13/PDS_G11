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

    public virtual DbSet<Cargo> Cargos { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Cpostal> Cpostals { get; set; }

    public virtual DbSet<EstadoGarantia> EstadoGarantia { get; set; }

    public virtual DbSet<EstadoManutencao> EstadoManutencaos { get; set; }

    public virtual DbSet<EstadoVeiculo> EstadoVeiculos { get; set; }

    public virtual DbSet<Fatura> Faturas { get; set; }

    public virtual DbSet<Funcionario> Funcionarios { get; set; }

    public virtual DbSet<Garantia> Garantia { get; set; }

    public virtual DbSet<Manutencao> Manutencaos { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Modelo> Modelos { get; set; }

    public virtual DbSet<Morada> Morada { get; set; }

    public virtual DbSet<Motor> Motors { get; set; }

    public virtual DbSet<TipoVeiculo> TipoVeiculos { get; set; }

    public virtual DbSet<Veiculo> Veiculos { get; set; }

    public virtual DbSet<Venda> Vendas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-KDTISKM3;Initial Catalog=fs;Integrated Security=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(e => e.IdCargo).HasName("PK__Cargo__8D69B95F793828B8");

            entity.ToTable("Cargo");

            entity.Property(e => e.IdCargo).HasColumnName("ID_Cargo");
            entity.Property(e => e.Nomecargo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nomecargo");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Cliente__E005FBFFCC543F5C");

            entity.ToTable("Cliente");

            entity.Property(e => e.IdCliente).HasColumnName("ID_Cliente");
            entity.Property(e => e.Contato)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("contato");
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
                .HasConstraintName("FK__Cliente__ID_Mora__29572725");
        });

        modelBuilder.Entity<Cpostal>(entity =>
        {
            entity.HasKey(e => e.IdCpostal).HasName("PK__CPostal__D39F2F58D16136CB");

            entity.ToTable("CPostal");

            entity.Property(e => e.IdCpostal).HasColumnName("ID_CPostal");
            entity.Property(e => e.Localidade)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("localidade");
        });

        modelBuilder.Entity<EstadoGarantia>(entity =>
        {
            entity.HasKey(e => e.IdEstadoGarantia).HasName("PK__EstadoGa__E954DE580519BD30");

            entity.Property(e => e.IdEstadoGarantia).HasColumnName("ID_EstadoGarantia");
            entity.Property(e => e.Descricaoestadogarantia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricaoestadogarantia");
        });

        modelBuilder.Entity<EstadoManutencao>(entity =>
        {
            entity.HasKey(e => e.IdEstadoManutencao).HasName("PK__EstadoMa__4497AA237490AB17");

            entity.ToTable("EstadoManutencao");

            entity.Property(e => e.IdEstadoManutencao).HasColumnName("ID_EstadoManutencao");
            entity.Property(e => e.Descricaoestadomanutencao)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricaoestadomanutencao");
        });

        modelBuilder.Entity<EstadoVeiculo>(entity =>
        {
            entity.HasKey(e => e.IdEstadoVeiculo).HasName("PK__EstadoVe__450DCABD4FCE6FD2");

            entity.ToTable("EstadoVeiculo");

            entity.Property(e => e.IdEstadoVeiculo).HasColumnName("ID_EstadoVeiculo");
            entity.Property(e => e.Descricaoestadoveiculo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricaoestadoveiculo");
        });

        modelBuilder.Entity<Fatura>(entity =>
        {
            entity.HasKey(e => e.IdFatura).HasName("PK__Fatura__9F2CBCBD0AECF775");

            entity.ToTable("Fatura");

            entity.Property(e => e.IdFatura).HasColumnName("ID_Fatura");
            entity.Property(e => e.Dataemissao)
                .HasColumnType("datetime")
                .HasColumnName("dataemissao");
            entity.Property(e => e.IdCliente).HasColumnName("ID_Cliente");
            entity.Property(e => e.IdManutencao).HasColumnName("ID_Manutencao");
            entity.Property(e => e.IdVendas).HasColumnName("ID_Vendas");
            entity.Property(e => e.TipoPagamento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipoPagamento");
            entity.Property(e => e.Valorfatura)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("valorfatura");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Faturas)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Fatura__ID_Clien__5629CD9C");

            entity.HasOne(d => d.IdManutencaoNavigation).WithMany(p => p.Faturas)
                .HasForeignKey(d => d.IdManutencao)
                .HasConstraintName("FK__Fatura__ID_Manut__5812160E");

            entity.HasOne(d => d.IdVendasNavigation).WithMany(p => p.Faturas)
                .HasForeignKey(d => d.IdVendas)
                .HasConstraintName("FK__Fatura__ID_Venda__571DF1D5");
        });

        modelBuilder.Entity<Funcionario>(entity =>
        {
            entity.HasKey(e => e.IdFuncionario).HasName("PK__Funciona__0AE977B9E73C88B7");

            entity.ToTable("Funcionario");

            entity.Property(e => e.IdFuncionario).HasColumnName("ID_Funcionario");
            entity.Property(e => e.Contato)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("contato");
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
            entity.Property(e => e.Password)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.IdCargoNavigation).WithMany(p => p.Funcionarios)
                .HasForeignKey(d => d.IdCargo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Funcionar__ID_Ca__2E1BDC42");
        });

        modelBuilder.Entity<Garantia>(entity =>
        {
            entity.HasKey(e => e.IdGarantia).HasName("PK__Garantia__E1CE1E9E7C065563");

            entity.Property(e => e.IdGarantia).HasColumnName("ID_Garantia");
            entity.Property(e => e.Datafim)
                .HasColumnType("datetime")
                .HasColumnName("datafim");
            entity.Property(e => e.Datainicio)
                .HasColumnType("datetime")
                .HasColumnName("datainicio");
            entity.Property(e => e.IdEstadoGarantia).HasColumnName("ID_EstadoGarantia");
            entity.Property(e => e.IdVendas).HasColumnName("ID_Vendas");

            entity.HasOne(d => d.IdEstadoGarantiaNavigation).WithMany(p => p.Garantia)
                .HasForeignKey(d => d.IdEstadoGarantia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Garantia__ID_Est__4AB81AF0");

            entity.HasOne(d => d.IdVendasNavigation).WithMany(p => p.Garantia)
                .HasForeignKey(d => d.IdVendas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Garantia__ID_Ven__4BAC3F29");
        });

        modelBuilder.Entity<Manutencao>(entity =>
        {
            entity.HasKey(e => e.IdManutencao).HasName("PK__Manutenc__D49EF33EA6C02A6F");

            entity.ToTable("Manutencao");

            entity.Property(e => e.IdManutencao).HasColumnName("ID_Manutencao");
            entity.Property(e => e.Custo)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("custo");
            entity.Property(e => e.Datamanutencao)
                .HasColumnType("datetime")
                .HasColumnName("datamanutencao");
            entity.Property(e => e.Descricaomanutencao)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descricaomanutencao");
            entity.Property(e => e.IdEstadoManutencao).HasColumnName("ID_EstadoManutencao");
            entity.Property(e => e.IdFuncionario).HasColumnName("ID_Funcionario");
            entity.Property(e => e.IdGarantia).HasColumnName("ID_Garantia");
            entity.Property(e => e.IdVeiculo).HasColumnName("ID_Veiculo");
            entity.Property(e => e.Tipomanutencao)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipomanutencao");

            entity.HasOne(d => d.IdEstadoManutencaoNavigation).WithMany(p => p.Manutencaos)
                .HasForeignKey(d => d.IdEstadoManutencao)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Manutenca__ID_Es__52593CB8");

            entity.HasOne(d => d.IdFuncionarioNavigation).WithMany(p => p.Manutencaos)
                .HasForeignKey(d => d.IdFuncionario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Manutenca__ID_Fu__534D60F1");

            entity.HasOne(d => d.IdGarantiaNavigation).WithMany(p => p.Manutencaos)
                .HasForeignKey(d => d.IdGarantia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Manutenca__ID_Ga__5165187F");

            entity.HasOne(d => d.IdVeiculoNavigation).WithMany(p => p.Manutencaos)
                .HasForeignKey(d => d.IdVeiculo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Manutenca__ID_Ve__5070F446");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.IdMarca).HasName("PK__Marca__9B8F8DB2665ADA0D");

            entity.ToTable("Marca");

            entity.HasIndex(e => e.Descricaomarca, "UQ__Marca__3C3A3EB49DED54AC").IsUnique();

            entity.Property(e => e.IdMarca).HasColumnName("ID_Marca");
            entity.Property(e => e.Descricaomarca)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricaomarca");
        });

        modelBuilder.Entity<Modelo>(entity =>
        {
            entity.HasKey(e => e.IdModelo).HasName("PK__Modelo__813C23727BF037CB");

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
                .HasConstraintName("FK__Modelo__ID_Marca__37A5467C");
        });

        modelBuilder.Entity<Morada>(entity =>
        {
            entity.HasKey(e => e.IdMorada).HasName("PK__Morada__67BC26C8C79A422C");

            entity.Property(e => e.IdMorada).HasColumnName("ID_Morada");
            entity.Property(e => e.Andar)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("andar");
            entity.Property(e => e.IdCpostal).HasColumnName("ID_CPostal");
            entity.Property(e => e.Numero)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("numero");
            entity.Property(e => e.Rua)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("rua");

            entity.HasOne(d => d.IdCpostalNavigation).WithMany(p => p.Morada)
                .HasForeignKey(d => d.IdCpostal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Morada__ID_CPost__267ABA7A");
        });

        modelBuilder.Entity<Motor>(entity =>
        {
            entity.HasKey(e => e.IdMotor).HasName("PK__Motor__D556B4EA83D6729A");

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

        modelBuilder.Entity<TipoVeiculo>(entity =>
        {
            entity.HasKey(e => e.IdTipoVeiculo).HasName("PK__TipoVeic__E6BC50A11041C4F2");

            entity.ToTable("TipoVeiculo");

            entity.Property(e => e.IdTipoVeiculo).HasColumnName("ID_TipoVeiculo");
            entity.Property(e => e.Descricaotveiculo)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("descricaotveiculo");
        });

        modelBuilder.Entity<Veiculo>(entity =>
        {
            entity.HasKey(e => e.IdVeiculo).HasName("PK__Veiculo__808FFECF12F401CA");

            entity.ToTable("Veiculo");

            entity.HasIndex(e => e.Matricula, "UQ__Veiculo__30962D15121F3775").IsUnique();

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
            entity.Property(e => e.Dataaquisicao)
                .HasColumnType("datetime")
                .HasColumnName("dataaquisicao");
            entity.Property(e => e.Descricao)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("descricao");
            entity.Property(e => e.Fornecedor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("fornecedor");
            entity.Property(e => e.Foto)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("foto");
            entity.Property(e => e.IdEstadoVeiculo).HasColumnName("ID_EstadoVeiculo");
            entity.Property(e => e.IdFuncionario).HasColumnName("ID_Funcionario");
            entity.Property(e => e.IdModelo).HasColumnName("ID_Modelo");
            entity.Property(e => e.IdMotor).HasColumnName("ID_Motor");
            entity.Property(e => e.IdTipoVeiculo).HasColumnName("ID_TipoVeiculo");
            entity.Property(e => e.Matricula)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("matricula");
            entity.Property(e => e.Origemveiculo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("origemveiculo");
            entity.Property(e => e.Precovenda)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("precovenda");
            entity.Property(e => e.Quilometros).HasColumnName("quilometros");
            entity.Property(e => e.Valorpago)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("valorpago");

            entity.HasOne(d => d.IdEstadoVeiculoNavigation).WithMany(p => p.Veiculos)
                .HasForeignKey(d => d.IdEstadoVeiculo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Veiculo__ID_Esta__3E52440B");

            entity.HasOne(d => d.IdFuncionarioNavigation).WithMany(p => p.Veiculos)
                .HasForeignKey(d => d.IdFuncionario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Veiculo__ID_Func__412EB0B6");

            entity.HasOne(d => d.IdModeloNavigation).WithMany(p => p.Veiculos)
                .HasForeignKey(d => d.IdModelo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Veiculo__ID_Mode__403A8C7D");

            entity.HasOne(d => d.IdMotorNavigation).WithMany(p => p.Veiculos)
                .HasForeignKey(d => d.IdMotor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Veiculo__ID_Moto__3D5E1FD2");

            entity.HasOne(d => d.IdTipoVeiculoNavigation).WithMany(p => p.Veiculos)
                .HasForeignKey(d => d.IdTipoVeiculo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Veiculo__ID_Tipo__3F466844");
        });

        modelBuilder.Entity<Venda>(entity =>
        {
            entity.HasKey(e => e.IdVendas).HasName("PK__Vendas__9DE9C8704E19F3CD");

            entity.Property(e => e.IdVendas).HasColumnName("ID_Vendas");
            entity.Property(e => e.Datavenda)
                .HasColumnType("datetime")
                .HasColumnName("datavenda");
            entity.Property(e => e.IdCliente).HasColumnName("ID_Cliente");
            entity.Property(e => e.IdFuncionario).HasColumnName("ID_Funcionario");
            entity.Property(e => e.IdVeiculo).HasColumnName("ID_Veiculo");
            entity.Property(e => e.Valorvenda)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("valorvenda");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Venda)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Vendas__ID_Clien__44FF419A");

            entity.HasOne(d => d.IdFuncionarioNavigation).WithMany(p => p.Venda)
                .HasForeignKey(d => d.IdFuncionario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Vendas__ID_Funci__45F365D3");

            entity.HasOne(d => d.IdVeiculoNavigation).WithMany(p => p.Venda)
                .HasForeignKey(d => d.IdVeiculo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Vendas__ID_Veicu__440B1D61");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
