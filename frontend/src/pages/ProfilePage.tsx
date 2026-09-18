import { useEffect, useState } from 'react'
import { Link } from '@tanstack/react-router'
import {
    changePassword,
    getCurrentUser,
    updateProfile,
} from '../services/auth'

function ProfilePage() {
    const [name, setName] = useState('')
    const [email, setEmail] = useState('')

    const [currentPassword, setCurrentPassword] = useState('')
    const [newPassword, setNewPassword] = useState('')
    const [confirmPassword, setConfirmPassword] = useState('')

    const [profileMessage, setProfileMessage] = useState('')
    const [profileError, setProfileError] = useState('')

    const [passwordMessage, setPasswordMessage] = useState('')
    const [passwordError, setPasswordError] = useState('')

    useEffect(() => {
        async function loadProfile() {
            const user = await getCurrentUser()

            if (user) {
                setName(user.name)
                setEmail(user.email)
            }
        }

        loadProfile()
    }, [])

    async function handleProfileSubmit(
        event: React.SubmitEvent<HTMLFormElement>
    ) {
        event.preventDefault()

        setProfileMessage('')
        setProfileError('')

        try {
            const updatedUser = await updateProfile({
                name: name.trim(),
                email: email.trim(),
            })

            setName(updatedUser.name)
            setEmail(updatedUser.email)
            setProfileMessage('Profile updated successfully.')
        } catch (error) {
            setProfileError(
                error instanceof Error
                    ? error.message
                    : 'Could not update profile.'
            )
        }
    }

    async function handlePasswordSubmit(
        event: React.SubmitEvent<HTMLFormElement>
    ) {
        event.preventDefault()

        setPasswordMessage('')
        setPasswordError('')

        if (newPassword !== confirmPassword) {
            setPasswordError('New passwords do not match.')
            return
        }

        try {
            await changePassword({
                currentPassword,
                newPassword,
            })

            setCurrentPassword('')
            setNewPassword('')
            setConfirmPassword('')

            setPasswordMessage('Password changed successfully.')
        } catch (error) {
            setPasswordError(
                error instanceof Error
                    ? error.message
                    : 'Could not change password.'
            )
        }
    }

    return (
        <main className="profile-page">
            <div className="profile-container">
                <div className="profile-header">
                    <div>
                        <h1>Account Settings</h1>
                        <p>Manage your personal information and password.</p>
                    </div>

                    <Link to="/dashboard" className="profile-back-link">
                        Back to board
                    </Link>
                </div>

                <section className="profile-section">
                    <h2>Personal Information</h2>

                    <form onSubmit={handleProfileSubmit}>
                        <label>
                            Name
                            <input
                                type="text"
                                value={name}
                                onChange={(event) => setName(event.target.value)}
                                required
                            />
                        </label>

                        <label>
                            Email
                            <input
                                type="email"
                                value={email}
                                onChange={(event) => setEmail(event.target.value)}
                                required
                            />
                        </label>

                        {profileError && (
                            <p className="profile-error">{profileError}</p>
                        )}

                        {profileMessage && (
                            <p className="profile-success">{profileMessage}</p>
                        )}

                        <button type="submit">Save changes</button>
                    </form>
                </section>

                <section className="profile-section">
                    <h2>Change Password</h2>

                    <form onSubmit={handlePasswordSubmit}>
                        <label>
                            Current password
                            <input
                                type="password"
                                value={currentPassword}
                                onChange={(event) =>
                                    setCurrentPassword(event.target.value)
                                }
                                required
                            />
                        </label>

                        <label>
                            New password
                            <input
                                type="password"
                                value={newPassword}
                                onChange={(event) =>
                                    setNewPassword(event.target.value)
                                }
                                required
                            />
                        </label>

                        <label>
                            Confirm new password
                            <input
                                type="password"
                                value={confirmPassword}
                                onChange={(event) =>
                                    setConfirmPassword(event.target.value)
                                }
                                required
                            />
                        </label>

                        {passwordError && (
                            <p className="profile-error">{passwordError}</p>
                        )}

                        {passwordMessage && (
                            <p className="profile-success">{passwordMessage}</p>
                        )}

                        <button type="submit">Change password</button>
                    </form>
                </section>
            </div>
        </main>
    )
}

export default ProfilePage