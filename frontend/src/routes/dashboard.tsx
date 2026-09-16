import { createFileRoute, redirect } from '@tanstack/react-router'
import DashboardPage from '../pages/DashboardPage'
import { getCurrentUser } from '../services/auth'

export const Route = createFileRoute('/dashboard')({
  beforeLoad: async () => {
    const user = await getCurrentUser()

    if (!user) {
      throw redirect({
        to: '/login',
      })
    }
  },

  component: DashboardPage,
})