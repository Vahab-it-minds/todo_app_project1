import { useState } from 'react'

function LoginPage() {
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [error, setError] = useState('')

  async function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault()
    setError('')

    try {
      const response = await fetch('http://localhost:5000/auth/login', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          email,
          password,
        }),
      })

      if (!response.ok) {
        setError('Invalid email or password.')
        return
      }

      const data = await response.json()

      localStorage.setItem('token', data.token)

      console.log('Login successful')
    } catch (error) {
      console.error(error)
      setError('Could not connect to the server.')
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