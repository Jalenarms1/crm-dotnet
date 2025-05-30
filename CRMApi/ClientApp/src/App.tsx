
import './App.css'
import { UserContextProvider, useUser } from './context/UserContext'
import AuthPage from './Pages/AuthPage'
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'

function App() {

  return (
    <>
      <UserContextProvider>
        <Home />

      </UserContextProvider>
    </>
  )
}

const Home = () => {
  const user = useUser()
  return (
    <>
      {user ? <AuthPage /> 
      : <Router>
          <Routes>
            <Route path='/' element={<p>Hello</p>} />
          </Routes>
        </Router>
      }
    </>
  )
}

export default App
