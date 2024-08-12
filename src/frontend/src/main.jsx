import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import App from './App.jsx'
import './index.css'
import {
  makeStyles,
  tokens,
  webLightTheme,
  FluentProvider,
} from "@fluentui/react-components";

const useStyles = makeStyles({
  button: {
    marginTop: "5px",
  },
  provider: {
    border: "1px",
    borderRadius: "5px",
    padding: "5px",
  },
  text: {
    backgroundColor: tokens.colorBrandBackground2,
    color: tokens.colorBrandForeground2,
    fontSize: "20px",
    border: "1px",
    borderRadius: "5px",
    padding: "5px",
  },
});

const root = createRoot(document.getElementById('root'));

export const Main = () => {
  const styles = useStyles();
  return(
    <FluentProvider className={styles.provider} theme={webLightTheme}>
    <StrictMode>
      <App />
    </StrictMode>
  </FluentProvider>
  );
}


root.render(<Main />);

