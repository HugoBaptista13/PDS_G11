using FaiscaSync.Models;
using FaiscaSync.DTO;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class VeiculoServices
    {
        private readonly FsContext _context;

        public VeiculoServices(FsContext context)
        {
            _context = context;
        }

        public async Task<List<Veiculo>> GetVeiculos()
        {
            return await _context.Veiculos.ToListAsync();
        }

        public async Task<Veiculo> GetVeiculo(int id)
        {
            var veiculo = await _context.Veiculos.FindAsync(id);
            if (veiculo == null)
            {
                throw new KeyNotFoundException($"Veiculo with ID {id} not found.");
            }
            return veiculo;
        }

        public async Task<IEnumerable<VeiculoPesquisaResultadoDTO>> PesquisaAvancadaAsync(PesquisaVeiculoDTO filtro)
        {
            var query = from v in _context.Veiculos

                        join modelo in _context.Modelos
                            on v.IdModelo equals modelo.IdModelo into modeloJoin
                        from modelo in modeloJoin.DefaultIfEmpty()

                        join marca in _context.Marcas
                            on modelo.IdMarca equals marca.IdMarca into marcaJoin
                        from marca in marcaJoin.DefaultIfEmpty()

                        join tipo in _context.TipoVeiculos
                            on v.IdTipoVeiculo equals tipo.IdTipoVeiculo into tipoJoin
                        from tipo in tipoJoin.DefaultIfEmpty()

                        join motor in _context.Motors
                            on v.IdMotor equals motor.IdMotor into motorJoin
                        from motor in motorJoin.DefaultIfEmpty()

                        select new VeiculoPesquisaResultadoDTO
                        {
                            IdVeiculo = v.IdVeiculo,
                            Matricula = v.Matricula,
                            Modelo = modelo != null ? modelo.Nomemodelo : null,
                            Marca = marca != null ? marca.Descricaomarca : null,
                            TipoVeiculo = tipo != null ? tipo.Descricaotveiculo : null,
                            Combustivel = motor != null ? motor.Combustivel : null,
                            Quilometragem = v.Quilometros,
                            PrecoVenda = v.Precovenda
                        };

            // Aplica filtros só se vierem no DTO
            if (!string.IsNullOrWhiteSpace(filtro.Marca))
                query = query.Where(x => x.Marca != null && x.Marca.Contains(filtro.Marca));

            if (!string.IsNullOrWhiteSpace(filtro.Modelo))
                query = query.Where(x => x.Modelo != null && x.Modelo.Contains(filtro.Modelo));

            if (!string.IsNullOrWhiteSpace(filtro.TipoVeiculo))
                query = query.Where(x => x.TipoVeiculo != null && x.TipoVeiculo.Contains(filtro.TipoVeiculo));

            if (!string.IsNullOrWhiteSpace(filtro.Combustivel))
                query = query.Where(x => x.Combustivel != null && x.Combustivel.Contains(filtro.Combustivel));

            if (filtro.AnoDe.HasValue)
                query = query.Where(x => x.PrecoVenda >= filtro.AnoDe);

            if (filtro.AnoAte.HasValue)
                query = query.Where(x => x.PrecoVenda <= filtro.AnoAte);

            if (filtro.PrecoDe.HasValue)
                query = query.Where(x => x.PrecoVenda >= filtro.PrecoDe);

            if (filtro.PrecoAte.HasValue)
                query = query.Where(x => x.PrecoVenda <= filtro.PrecoAte);
            if (filtro.QuilometrosDe.HasValue)
                query = query.Where(v => v.Quilometragem >= filtro.QuilometrosDe);

            if (filtro.QuilometrosAte.HasValue)
                query = query.Where(v => v.Quilometragem <= filtro.QuilometrosAte);

            return await query.ToListAsync();
        }



        public async Task<IEnumerable<Veiculo>> GetVeiculosDestaque()
        {
            return await _context.Veiculos
                .Include(v => v.IdModeloNavigation)
                .OrderByDescending(v => v.Dataaquisicao)
                .Take(5)
                .ToListAsync();
        }

        public async Task<List<Veiculo>> GetVeiculosDisponiveis()
        {
            return await _context.Veiculos
                .Include(v => v.IdModeloNavigation)
                .Include(v => v.IdEstadoVeiculoNavigation)
                .Where(v => v.IdEstadoVeiculoNavigation.Descricaoestadoveiculo == "Disponível")
                .ToListAsync();
        }

        public async Task<List<Veiculo>> GetVeiculosPorAprovar()
        {
            return await _context.Veiculos
                .Include(v => v.IdModeloNavigation)
                .Include(v => v.IdEstadoVeiculoNavigation)
                .Where(v => v.IdEstadoVeiculoNavigation.Descricaoestadoveiculo == "Por aprovar")
                .ToListAsync();
        }



        public async Task<Veiculo> CriarVeiculoCompletoAsync(VeiculoDTO dto, int idFuncionario)
        {
            // Verifica se já existe Veículo com a mesma matrícula
            var exists = await _context.Veiculos
                .AnyAsync(v => v.Matricula == dto.Matricula);

            if (exists)
            {
                throw new InvalidOperationException($"Já existe um veículo com a matrícula {dto.Matricula}.");
            }

            // 1️⃣ Marca
            var marca = await _context.Marcas
                .FirstOrDefaultAsync(m => m.Descricaomarca == dto.DescricaoMarca);

            if (marca == null)
            {
                marca = new Marca
                {
                    Descricaomarca = dto.DescricaoMarca
                };
                _context.Marcas.Add(marca);
                await _context.SaveChangesAsync();  // para gerar IdMarca
            }

            // 2️⃣ Modelo
            var modelo = await _context.Modelos
                .FirstOrDefaultAsync(m => m.Nomemodelo == dto.NomeModelo && m.IdMarca == marca.IdMarca);

            if (modelo == null)
            {
                modelo = new Modelo
                {
                    Nomemodelo = dto.NomeModelo,
                    IdMarca = marca.IdMarca
                };
                _context.Modelos.Add(modelo);
                await _context.SaveChangesAsync();  // gera IdModelo
            }

            // 3️⃣ Motor
            var motor = await _context.Motors
                .FirstOrDefaultAsync(m => m.Tipomotor == dto.TipoMotor && m.Potencia == dto.Potencia && m.Combustivel == dto.Combustivel);

            if (motor == null)
            {
                motor = new Motor
                {
                    Tipomotor = dto.TipoMotor,
                    Potencia = dto.Potencia,
                    Combustivel = dto.Combustivel
                };
                _context.Motors.Add(motor);
                await _context.SaveChangesAsync();  // gera IdMotor
            }

            // 4️⃣ Estado "Por aprovar"
            var estadoPorAprovar = await _context.EstadoVeiculos
                .FirstOrDefaultAsync(e => e.Descricaoestadoveiculo == "Por aprovar");

            if (estadoPorAprovar == null)
                throw new Exception("Estado 'Por aprovar' não encontrado na base de dados.");

            // 5️⃣ Criar o Veículo
            var veiculo = new Veiculo
            {
                Matricula = dto.Matricula,
                Chassi = dto.Chassi,
                Anofabrico = dto.AnoFabrico,
                Cor = dto.Cor,
                Quilometros = dto.Quilometragem,
                Precovenda = dto.PrecoVenda,
                Fornecedor = dto.Fornecedor,
                Valorpago = dto.ValorPago,
                Dataaquisicao = dto.DataAquisicao,
                Origemveiculo = dto.OrigemVeiculo,
                Foto = dto.Foto,
                Descricao = dto.Descricao,
                IdMotor = motor.IdMotor,
                IdEstadoVeiculo = estadoPorAprovar.IdEstadoVeiculo,  // SEMPRE "Por aprovar"
                IdTipoVeiculo = dto.IdTipoVeiculo,
                IdModelo = modelo.IdModelo,
                IdFuncionario = idFuncionario
            };

            _context.Veiculos.Add(veiculo);
            await _context.SaveChangesAsync();

            return veiculo;
        }


        public async Task<bool> AprovarVeiculoAsync(int veiculoId)
        {
            var veiculo = await _context.Veiculos.FindAsync(veiculoId);
            if (veiculo == null)
                return false;

            var estadoDisponivel = await _context.EstadoVeiculos
                .FirstOrDefaultAsync(e => e.Descricaoestadoveiculo == "Disponível");

            if (estadoDisponivel != null)
            {
                veiculo.IdEstadoVeiculo = estadoDisponivel.IdEstadoVeiculo;
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateAsync(int id, Veiculo veiculo)
        {
            var existing = await _context.Veiculos.FindAsync(id);
            if (existing == null)
                return false;

            _context.Entry(existing).CurrentValues.SetValues(veiculo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var veiculo = await _context.Veiculos.FindAsync(id);
            if (veiculo == null)
                return false;

            _context.Veiculos.Remove(veiculo);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
