import { Module } from '@nestjs/common';
import { ManutencoesController } from './manutencoes.controller';
import { ManutencoesService } from './manutencoes.service';

@Module({
  controllers: [ManutencoesController],
  providers: [ManutencoesService]
})
export class ManutencoesModule {}
