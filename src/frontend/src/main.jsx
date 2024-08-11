import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { FluentProvider, webLightTheme } from '@fluentui/react-components';
import App from './App.jsx'
import './index.css'

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <App />
  </StrictMode>,
)

const root = createRoot(document.getElementById('root'));

root.render(
  <FluentProvider theme={webLightTheme}>
    <StrictMode>
      <App />
    </StrictMode>
  </FluentProvider>,
);