import { Module } from '@nestjs/common';
import { GarantiasController } from './garantias.controller';
import { GarantiasService } from './garantias.service';

@Module({
  controllers: [GarantiasController],
  providers: [GarantiasService]
})
export class GarantiasModule {}
