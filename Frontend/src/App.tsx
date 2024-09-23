import './App.css'
import { BrowserRouter, Routes, Route } from "react-router-dom";
import DemoPage from './pages/demopage';

function App() {

  return (
    <BrowserRouter>
      <Routes>
        <Route path='/' element={<DemoPage />} />
      </Routes>
    </BrowserRouter>
  )
}

export default App
