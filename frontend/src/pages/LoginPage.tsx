import { useNavigate } from '@tanstack/react-router'
import { useState } from 'react'
import { login } from '../services/auth'


function LoginPage() {
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [error, setError] = useState('')
  const navigate = useNavigate()

async function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
  event.preventDefault()
  setError('')

  try {
    await login(email, password)

    navigate({
      to: '/dashboard',
    })
  } catch (error) {
    console.error(error)
    setError('Invalid email or password.')
  }
}

  return (
    <div className="login-container">
      <h1>Welcome</h1>
      <p>Log in to your account</p>

      <form onSubmit={handleSubmit}>
        <div className="form-group">
          <label htmlFor="email">Email</label>
          <input
            id="email"
            type="email"
            value={email}
            onChange={(event) => setEmail(event.target.value)}
            required
          />
        </div>

        <div className="form-group">
          <label htmlFor="password">Password</label>
          <input
            id="password"
            type="password"
            value={password}
            onChange={(event) => setPassword(event.target.value)}
            required
          />
        </div>

        <a href="#">Forgot password?</a>

        {error && <p className="login-error">{error}</p>}

        <button type="submit">Log in</button>
      </form>

      <p>
        Don't have an account? <a href="#">Sign up</a>
      </p>
    </div>
  )
}

export default LoginPage