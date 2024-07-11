import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';
import { ServiceBase } from '../services/ServiceBase';
import { CategoryService } from '../services/Categories/CategoryService';
import { CommonModule } from '@angular/common';
import { CategoryComponent } from '../components/category/category.component';

export const appConfig: ApplicationConfig = {
  providers: [
      provideZoneChangeDetection({ eventCoalescing: true }),
      provideRouter(routes),
      provideHttpClient(),
      ServiceBase,
      CategoryService,
      CommonModule,
      CategoryComponent
    ]
};
