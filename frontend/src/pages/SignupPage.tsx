import { useState } from 'react'
import { Link, useNavigate } from '@tanstack/react-router'
import { login, signup } from '../services/auth'

function SignupPage() {
  const navigate = useNavigate()

  const [name, setName] = useState('')
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [error, setError] = useState('')
  const [isSubmitting, setIsSubmitting] = useState(false)

  async function handleSubmit(
    event: React.SubmitEvent<HTMLFormElement>
  ) {
    event.preventDefault()

    try {
      setIsSubmitting(true)
      setError('')

      await signup({
        name: name.trim(),
        email: email.trim(),
        password,
      })

      await login(email.trim(), password)

      await navigate({
        to: '/dashboard',
      })
    } catch (error) {
      if (error instanceof Error) {
        setError(error.message)
      } else {
        setError('Could not create account.')
      }
    } finally {
      setIsSubmitting(false)
    }
  }

  return (
    <main className="login-container">
      <h1>Create account</h1>
      <p>Create an account to manage your todos.</p>

      <form onSubmit={handleSubmit}>
        <div className="form-group">
          <label htmlFor="name">Name</label>

          <input
            id="name"
            type="text"
            value={name}
            onChange={(event) => setName(event.target.value)}
            required
          />
        </div>

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

        {error && (
          <p className="login-error">
            {error}
          </p>
        )}

        <button
          type="submit"
          disabled={isSubmitting}
        >
          {isSubmitting ? 'Creating account...' : 'Create account'}
        </button>
      </form>

      <p>
        Already have an account?{' '}
        <Link to="/login">
          Log in
        </Link>
      </p>
    </main>
  )
}

export default SignupPage