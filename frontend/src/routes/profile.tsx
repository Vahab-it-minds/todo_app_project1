import {
    createFileRoute,
    redirect,
} from '@tanstack/react-router'
import ProfilePage from '../pages/ProfilePage'
import { getCurrentUser } from '../services/auth'

export const Route = createFileRoute('/profile')({
    beforeLoad: async () => {
        const user = await getCurrentUser()

        if (!user) {
            throw redirect({
                to: '/login',
            })
        }
    },

    component: ProfilePage,
})