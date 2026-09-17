import { useEffect, useState } from 'react'
import { getTodos, type Todo } from '../services/todos'
import KanbanColumn from '../components/KanbanColumn'
import CreateTodoModal from '../components/CreateTodoModal'
import EditTodoModal from '../components/EditTodoModal'
import { useNavigate } from '@tanstack/react-router'
import { logout } from '../services/auth'

function DashboardPage() {
  const [todos, setTodos] = useState<Todo[]>([])
  const [isLoading, setIsLoading] = useState(true)
  const [error, setError] = useState('')
  const [isCreateModalOpen, setIsCreateModalOpen] = useState(false)
  const [selectedTodo, setSelectedTodo] = useState<Todo | null>(null)
  const navigate = useNavigate()

  useEffect(() => {
    getTodos()
      .then((data) => {
        setTodos(data)
      })
      .catch((error) => {
        console.error(error)
        setError('Could not load your todos.')
      })
      .finally(() => {
        setIsLoading(false)
      })
  }, [])

  const todoTasks = todos.filter(
    (todo) => todo.status === 'Todo'
  )

  const inProgressTasks = todos.filter(
    (todo) => todo.status === 'InProgress'
  )

  const doneTasks = todos.filter(
    (todo) => todo.status === 'Done'
  )

  async function handleLogout() {
    await logout()

    await navigate({
      to: '/login',
    })
  }

  if (isLoading) {
    return <p>Loading todos...</p>
  }

  if (error) {
    return <p>{error}</p>
  }

  return (
    <main className="kanban-page">
      <div className="kanban-page-header">
        <div>
          <h1>My Todos</h1>
          <p>Manage your tasks and track your progress.</p>
        </div>

        <div className="kanban-header-actions">
          <button
            className="logout-button"
            onClick={handleLogout}
          >
            Log out
          </button>

          <button
            className="create-todo-button"
            onClick={() => setIsCreateModalOpen(true)}
          >
            + New Todo
          </button>
        </div>
      </div>

      <div className="kanban-board">
        <KanbanColumn
          title="To Do"
          todos={todoTasks}
          onTodoClick={setSelectedTodo}
        />

        <KanbanColumn
          title="In Progress"
          todos={inProgressTasks}
          onTodoClick={setSelectedTodo}
        />

        <KanbanColumn
          title="Done"
          todos={doneTasks}
          onTodoClick={setSelectedTodo}
        />
      </div>

      {isCreateModalOpen && (
        <CreateTodoModal
          onClose={() => setIsCreateModalOpen(false)}
          onTodoCreated={(newTodo) => {
            setTodos((currentTodos) => [
              ...currentTodos,
              newTodo,
            ])
          }}
        />
      )}

      {selectedTodo && (
        <EditTodoModal
          todo={selectedTodo}
          onClose={() => setSelectedTodo(null)}
          onTodoUpdated={(updatedTodo) => {
            setTodos((currentTodos) =>
              currentTodos.map((todo) =>
                todo.id === updatedTodo.id
                  ? updatedTodo
                  : todo
              )
            )
         }}
        onTodoDeleted={(deletedTodoId) => {
          setTodos((currentTodos) =>
            currentTodos.filter(
              (todo) => todo.id !== deletedTodoId
            )
          )
        }}
        />
      )}
    </main>
  )
}

export default DashboardPage