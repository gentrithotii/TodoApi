import './App.css'
import { BrowserRouter, Routes, Route } from "react-router-dom";
import DemoPage from './pages/demopage';

function App() {

  return (
    <BrowserRouter>
      {/* <Providors> */}
      <Routes>
        <Route path='/' element={<DemoPage />} />
      </Routes>
      {/* <Providor /> */}
    </BrowserRouter>
  )
}

export default App
