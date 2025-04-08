import { Module } from '@nestjs/common';
import { ConfigModule } from '@nestjs/config';
import { TypeOrmModule } from '@nestjs/typeorm';
import { AppController } from './app.controller';
import { AppService } from './app.service';
import { ClientesModule } from './modules/clientes/clientes.module';
import { VeiculosModule } from './modules/veiculos/veiculos.module';
import { VendasModule } from './modules/vendas/vendas.module';
import { FuncionariosModule } from './modules/funcionarios/funcionarios.module';
import { FaturasModule } from './modules/faturas/faturas.module';
import { ManutencoesModule } from './modules/manutencoes/manutencoes.module';
import { GarantiasModule } from './modules/garantias/garantias.module';
import { PagamentosModule } from './modules/pagamentos/pagamentos.module';

@Module({
  imports: [
    ConfigModule.forRoot({ isGlobal: true }),
    TypeOrmModule.forRoot({
      type: 'mssql',
      host: process.env.DB_HOST,
      port: Number(process.env.DB_PORT) || 1433,
      database: process.env.DB_DATABASE,
      synchronize: process.env.DB_SYNCHRONIZE === 'true',
      extra: {
        trustServerCertificate: process.env.DB_TRUST_SERVER_CERTIFICATE === 'true',
      },
      options: process.env.DB_USE_WINDOWS_AUTH === 'true' ? {
        encrypt: true,
      } : undefined,
      autoLoadEntities: true,
    }),
    ClientesModule,
    VeiculosModule,
    VendasModule,
    FuncionariosModule,
    FaturasModule,
    ManutencoesModule,
    GarantiasModule,
    PagamentosModule,
  ],
  controllers: [AppController],
  providers: [AppService],
})
export class AppModule {}