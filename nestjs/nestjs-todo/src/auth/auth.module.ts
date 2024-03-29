import { Module } from '@nestjs/common';
import { AuthService } from './auth.service';
import { AuthController } from './auth.controller';
import { AuthConfig } from './auth.config';

@Module({
  providers: [AuthConfig, AuthService],
  controllers: [AuthController],
})
export class AuthModule {}
