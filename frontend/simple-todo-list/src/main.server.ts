

import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { config } from './app/app.config.server';
// Removed type import for BootstrapContext as it is not exported by @angular/platform-server

// @ts-ignore
const bootstrap = (context) =>
	bootstrapApplication(AppComponent, config, context);

export default bootstrap;
