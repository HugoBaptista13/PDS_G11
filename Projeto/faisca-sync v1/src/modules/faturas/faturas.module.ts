import { Module } from '@nestjs/common';
import { FaturasController } from './faturas.controller';
import { FaturasService } from './faturas.service';

@Module({
  controllers: [FaturasController],
  providers: [FaturasService]
})
export class FaturasModule {}
