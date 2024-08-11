# CronScheduler.AspNetCore "Accelerator" - Frontend

This project is part of the CronScheduler.AspNetCore "Accelerator" solution. It provides the frontend interface for managing and viewing cron jobs using React and Vite.

## Table of Contents

- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [Configuration](#configuration)
- [Available Scripts](#available-scripts)
- [Running the Application](#running-the-application)
- [Contributing](#contributing)
- [License](#license)

## Getting Started

To get started with this project, you will need to have the following tools installed:

- [Node.js](https://nodejs.org/) (version 16 or later)
- [npm](https://www.npmjs.com/) (version 7 or later)

## Project Structure

The project is organized as follows:

- `public/`: Contains static assets such as images and icons.
- `src/`: Contains the source code for the React application.
  - `assets/`: Contains static assets used in the application.
  - `components/`: Contains React components.
  - `pages/`: Contains the main pages of the application.
  - `App.jsx`: The main application component.
  - `main.jsx`: The entry point for the React application.
- `index.html`: The main HTML file for the application.

## Configuration

Configuration settings for the application are stored in the `vite.config.js` file. This file includes settings for the development server, build options, and plugins.

## Available Scripts

In the project directory, you can run the following scripts:

### `npm install`

Installs the project dependencies.

### `npm run dev`

Runs the application in development mode. Open [http://localhost:5173](http://localhost:5173) to view it in the browser. The page will reload if you make edits.

### `npm run build`

Builds the application for production to the `dist` folder. It correctly bundles React in production mode and optimizes the build for the best performance.

### `npm run preview`

Serves the production build locally. This is useful for testing the production build before deploying.

## Running the Application

To run the application, use the following commands:

```bash
npm install
npm run dev
```

This will start the application and make it available at `http://localhost:5173`.

## Contributing

Contributions are welcome! Please read the [contributing guidelines](../CONTRIBUTING.md) for more information.

## License

This project is licensed under the MIT License. See the [LICENSE](../LICENSE) file for details.
