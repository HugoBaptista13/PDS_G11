import { Module } from '@nestjs/common';
import { PagamentosController } from './pagamentos.controller';
import { PagamentosService } from './pagamentos.service';

@Module({
  controllers: [PagamentosController],
  providers: [PagamentosService]
})
export class PagamentosModule {}
